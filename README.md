#Examen Mercadolibre
##Problema
Magneto quiere reclutar la mayor cantidad de mutantes para poder luchar contra los X-Men.
Te ha contratado a ti para que desarrolles un proyecto que detecte si unhumano es mutante basándose en su secuencia de ADN.
Para eso te ha pedido crear un programa con un método o función con la siguiente firma (En alguno de los siguiente lenguajes: Java / Golang / C-C++ / Javascript (node) / Python / Ruby):

```java
boolean isMutant(String[] dna); // Ejemplo Java
```
En donde recibirás como parámetro un array de Strings que representan cada fila de una tabla de (NxN) con la secuencia del ADN. Las letras de los Strings solo pueden ser: (A,T,C,G), las cuales representa cada base nitrogenada del ADN.

Sabrás si un humano es mutante, si encuentras más de una secuencia de cuatro letras
iguales , de forma oblicua, horizontal o vertical.
**Ejemplo (Caso mutante):**
```
String[] dna = {"ATGCGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCACTG"};
```
En este caso el llamado a la función isMutant(dna) devuelve “true”.

#Solución
la solución fue desplegada en Azure como aplicacion web bajo el siguiente dominio:
https://app-meliapirestdna-prod.azurewebsites.net/swagger/index.html

##Instalación
###Pre-requisitos
* [MongoDb](https://www.mongodb.com/try/download/community)
* [Net Core SDK 3.1](https://dotnet.microsoft.com/download)

Clonar repositorio 
`git clone https://github.com/waruano/meli-apirestdna.git`

Configurar la cadena de conexión de mongo en `Meli.ApiRestDNA\appsettings.<env>.json`
```json
 "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "MeliDatabase"
  },
```
Por defecto `env` tiene el valor `Development`

Abrir con un editor de texto el archivo `Meli.ApiRestDNA.csproj`
Cambiar la ruta del archivo `Meli.ApiRestDNA.xml` el cual contiene la documentacion del api.
```xml
<PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
    <DocumentationFile>my\project\path\Meli.ApiRestDNA.xml</DocumentationFile>
    <NoWarn>1701;1702;1591</NoWarn>
  </PropertyGroup>
```
Guardar el archivo.

Abrir una terminal en la carpeta `Meli.ApiRestDNA`, ejecutar el comando: `dotnet run`


## Nivel 1
El método pedido por magneto esta implementado en:
https://github.com/waruano/meli-apirestdna/blob/master/Meli.ApiRestDNA/Application/Services/DnaValidatorByChar.cs

## Nivel 2
## Detectar mutante

Valida si un humano es un mutante a partir de su secuencia de ADN.

**URL** : `https://app-meliapirestdna-prod.azurewebsites.net/api/v1/Mutant`

**Verbo/Método** : `POST`

**Autenticación** : NO

**Permisos** : None

**Data constraints**

La matriz de adn debe ser de tamaño NxN y el dominio de sus caracteres es `'A','T','G','C'`, no distingue mayúsculas y minúsculas.

```json
{
    "dna": []
}
```

**ADN Mutante** 

```json
{
    "dna": ["ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG"]
}
```
**ADN Humano** 

```json
{
    "dna": ["ATGCA", "TAGAT", "CCCTT", "CAGAG", "GGTTC"]
}
```

## Success Response

**Condición** : Si el adn enviado corresponde a un mutante

**Código** : `200 OK`

**Contenido**: None

## Error Responses

**Condición** : Si el adn enviado no tiene el formato correcto

**Código** : `400 BAD REQUEST`

**Contenido** : 
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "traceId": "|4badb3ab-4f20a6e253370b09.",
  "errors": {
    "Dna": [
      "invalid DNA format"
    ]
  }
}
```

### Or

**Condición** : If fields are missed.

**Código** : `403 FORBIDDEN`

**Contenido**

```json
{
  "message": "is a Human",
  "errorCode": 403,
  "details": "Business Exception"
}
```

## Nivel 3

###Base de Datos
Se anexa una base de datos mongo para persistir los adns validados, esta base de datos tiene dos colecciones:
**Humans**: Contiene la información del adn junto con una marca "isMutant" la cual indica si el humano es o no mutante.
**Report**: Guarda un único documento el cual es actualizado mediante un evento emitido por el guardado de cada documento correspondiente al humano. Esta colección puede ser reemplazada por un registro en algún servicio de cache distribuida como Redis Cache.

## Stats

Muestra estadisticas de los adns validados mediante el apirest

**URL** : `https://app-meliapirestdna-prod.azurewebsites.net/api/v1/Stats`

**Verbo/Método** : `GET`

**Autenticación** : NO

**Permisos** : None

## Success Response

**Condición** : Siempre que el apirest esté arriba

**Código** : `200 OK`

**Contenido**: 
```json
{
  "count_mutant_dna": 0,
  "count_human_dna": 0,
  "ratio": 0
}
```

## Error Responses

**Condición** : Si ocurre un error no controlado

**Código** : `500 INTERNAL SERVER ERROR`

**Contenido** : 
```json
{
  "message": "string",
  "errorCode": 0,
  "details": "string"
}
```
##Pruebas unitarias

El proyecto **Meli.ApiRestDNA** tiene una cobertura de `90.9%`

##Soporte de tráfico
La aplicación en el host donde está albergada actualmente no posee la capacidad para soportar las cantidades agresivas de tráfico presentadas en el problema, sin embargo, la aplicación es contenerizable por lo que podríamos pensar en un despliegue en k8s.