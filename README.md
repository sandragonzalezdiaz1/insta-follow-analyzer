# InstaFollowAnalyzer

InstaFollowAnalyzer es una aplicación de escritorio desarrollada en **C# con Windows Forms y .NET 10.0** que permite comparar los seguidores y seguidos de una cuenta de Instagram a partir de los archivos JSON exportados por Instagram.

La aplicación permite consultar:

- Personas que sigues.
- Personas que te siguen.
- Personas que sigues pero no te siguen de vuelta.
- Personas que te siguen pero tu no sigues.
- Seguidores mutuos.
- Acceso directo al perfil de cada usuario.
- Búsqueda por nombre de usuario.

## Estructura del proyecto

```text
InstaFollowAnalyzer
|
|-- Forms
|   |-- MainForm.cs
|   |-- MainForm.Designer.cs
|   `-- MainForm.resx
|
|-- Models
|   |-- FollowerItem.cs
|   |-- FollowingExport.cs
|   |-- FollowingItem.cs
|   `-- InstaStringData.cs
|
|-- Services
|   |-- FollowerComparisonService.cs
|   `-- InstaJsonService.cs
|
|-- Program.cs
|-- InstaFollowAnalyzer.csproj
`-- README.md
```

## Como obtener los archivos JSON de Instagram

InstaFollowAnalyzer no necesita tu contraseña ni accede directamente a tu cuenta de Instagram.

Los datos se obtienen utilizando la función oficial de Instagram para descargar una copia de tu información.

> La ubicación exacta de algunas opciones puede cambiar dependiendo de la versión de Instagram.

### 1. Abre el Centro de cuentas

Desde Instagram:

1. Accede a tu perfil.
2. Abre **Configuracion y actividad**.
3. Entra en **Centro de cuentas**.
4. Selecciona **Tu informacion y permisos**.
5. Pulsa **Descargar tu informacion**.

### 2. Selecciona tu cuenta de Instagram

Selecciona la cuenta de Instagram cuyos seguidores y seguidos quieras analizar.

Cuando Instagram pregunte que informacion quieres descargar, selecciona la opcion para elegir unicamente una parte de tu informacion.

### 3. Selecciona seguidores y seguidos

Dentro de las categorias disponibles, busca la información relacionada con tus conexiones y selecciona:

**Seguidores y seguidos**

No es necesario descargar toda la información de tu cuenta.

### 4. Selecciona el formato JSON

Cuando Instagram te permita configurar la descarga, selecciona:

**Formato: JSON**

Es importante utilizar JSON y no HTML, ya que InstaFollowAnalyzer procesa directamente estos archivos.

También puedes seleccionar el intervalo de fechas que corresponda. Si quieres obtener la información más completa posible de tu cuenta, selecciona todo el periodo disponible.

### 5. Solicita la descarga

Confirma la solicitud.

Instagram preparara un archivo con la información seleccionada. Este proceso puede tardar un tiempo.

Cuando este disponible, descarga el archivo `.zip` y descomprimelo en tu ordenador.

### 6. Localiza los archivos

Dentro de la descarga encontrarás una carpeta relacionada con seguidores y seguidos.

Los nombres pueden variar ligeramente según la versión de la exportación de Instagram, pero normalmente encontrarás archivos similares a:

```text
following.json
followers_1.json
```

- `following.json` contiene las cuentas que **sigues**.
- `followers_1.json` contiene las cuentas que **te siguen**.

Estos son los archivos que necesita InstaFollowAnalyzer.

## Como utilizar InstaFollowAnalyzer

Abre la aplicación y utiliza los botones de la parte superior:

```text
Cargar seguidos
Cargar seguidores
```

En **Cargar seguidos**, selecciona:

```text
following.json
```

En **Cargar seguidores**, selecciona:

```text
followers_1.json
```

Una vez cargados ambos archivos, InstaFollowAnalyzer comparará automaticamente la información y mostrará:

- **Sigues:** numero total de cuentas que sigues.
- **Te siguen:** numero total de seguidores.
- **No te siguen:** personas que sigues pero que no te siguen.
- **No sigues:** personas que te siguen pero que tu no sigues.
- **Mutuos:** personas que se siguen mutuamente contigo.

Puedes utilizar el buscador para localizar rapidamente un usuario y pulsar **Abrir** para acceder a su perfil de Instagram.

## Privacidad

Los archivos JSON se procesan **localmente en tu ordenador**.

InstaFollowAnalyzer no necesita:

- Tu usuario de Instagram.
- Tu contraseña.
- Iniciar sesión en Instagram.
- Conectarse a la API de Instagram.
- Subir tus archivos a un servidor externo.

Los datos utilizados por la aplicación permanecen en tu equipo.

## Tecnologias utilizadas

- C#
- .NET 10.0
- Windows Forms
- System.Text.Json
- LINQ
- FontAwesome.Sharp

## Aviso

InstaFollowAnalyzer es un proyecto independiente y no esta afiliado, asociado ni respaldado por Instagram o Meta.

La estructura y el procedimiento de exportación de datos de Instagram pueden cambiar en el futuro.
