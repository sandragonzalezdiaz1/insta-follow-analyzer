using InstaFollowAnalyzer.Models;
using System.Text.Json;

namespace InstaFollowAnalyzer.Services
{
    public class InstaJsonService
    {
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true // Permite reconocer las claves del JSON de Instagram aunque cambien las mayúscula
        };

        // Instagram guarda los seguidos dentro de un objeto relationships_following
        public async Task<List<string>> ReadFollowingUsernamesAsync(string filePath)
        {
            string json = await File.ReadAllTextAsync(filePath);
            ValidateFollowingJson(json);

            FollowingExport? export = JsonSerializer.Deserialize<FollowingExport>(json, _jsonOptions);

            return export?
                .RelationshipsFollowing
                .Select(item => item.Title)
                .Where(username => !string.IsNullOrWhiteSpace(username))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(username => username)
                .ToList()
                ?? [];
        }

        // Instagram guarda los seguidores como una lista raíz, con los usuarios dentro de string_list_data
        public async Task<List<string>> ReadFollowerUsernamesAsync(string filePath)
        {
            string json = await File.ReadAllTextAsync(filePath);
            ValidateFollowersJson(json);

            List<FollowerItem>? followers = JsonSerializer.Deserialize<List<FollowerItem>>(json, _jsonOptions);

            return followers?
                .SelectMany(item => item.StringListData)
                .Select(data => data.Value)
                .Where(username => !string.IsNullOrWhiteSpace(username))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(username => username)
                .ToList()
                ?? [];
        }

        // Comprueba que el archivo tenga la estructura esperada antes de deserializarlo
        private static void ValidateFollowingJson(string json)
        {
            using JsonDocument document = ParseJson(json);

            if (document.RootElement.ValueKind != JsonValueKind.Object ||
                !document.RootElement.TryGetProperty("relationships_following", out JsonElement followingElement) ||
                followingElement.ValueKind != JsonValueKind.Array)
            {
                throw new InvalidDataException(
                    "El archivo seleccionado no parece ser el JSON de seguidos. Selecciona el archivo que contiene 'relationships_following'."
                );
            }
        }

        private static void ValidateFollowersJson(string json)
        {
            using JsonDocument document = ParseJson(json);

            if (document.RootElement.ValueKind != JsonValueKind.Array)
            {
                throw new InvalidDataException(
                    "El archivo seleccionado no parece ser el JSON de seguidores. Selecciona el archivo que empieza con una lista de usuarios."
                );
            }

            if (document.RootElement.GetArrayLength() == 0)
                return;

            bool containsFollowerData = document.RootElement
                .EnumerateArray()
                .Any(item =>
                    item.ValueKind == JsonValueKind.Object &&
                    item.TryGetProperty("string_list_data", out JsonElement stringListData) &&
                    stringListData.ValueKind == JsonValueKind.Array &&
                    stringListData.EnumerateArray().Any(data =>
                        data.ValueKind == JsonValueKind.Object &&
                        data.TryGetProperty("value", out JsonElement value) &&
                        value.ValueKind == JsonValueKind.String &&
                        !string.IsNullOrWhiteSpace(value.GetString())
                    )
                );

            if (!containsFollowerData)
            {
                throw new InvalidDataException(
                    "El archivo seleccionado tiene formato JSON, pero no contiene la estructura esperada."
                );
            }
        }

        // Intenta parsear el JSON y lanza una excepción si no es válido
        private static JsonDocument ParseJson(string json)
        {
            try
            {
                return JsonDocument.Parse(json);
            }
            catch (JsonException)
            {
                throw new InvalidDataException(
                    "El archivo seleccionado no contiene un JSON válido. Revisa que hayas seleccionado un archivo exportado por Instagram."
                );
            }
        }


    }
}
