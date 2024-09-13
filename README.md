# SchoolDB API

## Contributeurs
Julien LAY - [GitHub](https://github.com/JulienLAY)

Romain PONS - [GitHub](https://github.com/romain-pons)

Adrien AUTEF - [GitHub](https://github.com/AdrienAUTEF)

## Description

SchoolDB API est un projet d'API RESTful développé en ASP.NET Core. Il permet la gestion d'une base de données de l'école, incluant des entités telles que les étudiants, les professeurs et les cours.

## Prérequis

Avant de lancer le projet, assurez-vous d'avoir installé les éléments suivants :

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/) (facultatif mais recommandé pour l'édition et le debug)
- Un client MySQL (tel que MySQL Workbench) pour gérer la base de données.

## Structure du projet
Controllers/ : Contient les contréleurs de l'API qui gérent les différentes routes pour les entités Etudiants, Profs, et Cours.

Models/ : Définit les modéles de données utilisés dans l'API.

Data/ : Contient le contexte de base de données (AppDbContext) et les services qui interagissent avec la base de données.

appsettings.json : Contient la configuration de l'application, notamment la chaîne de connexion à la base de données.

## Installation

### 1. Clonez le dépôt

Clonez le dépôt GitHub du projet à l'aide de la commande suivante :

```bash
git clone https://github.com/romain-pons/ProjetWebAPI.git
cd ProjetWebAPI
```

### 2. Configurer la base de données MySQL

Vous devez configurer une base de données MySQL pour le projet. Vous pouvez soit utiliser une base de données MySQL locale, soit une base de données MySQL hébergée sur le cloud, comme celle mentionnée dans appsettings.json.

Créer la base de données
Si vous n'avez pas encore de base de données, connectez-vous à votre serveur MySQL et exécutez la commande suivante pour créer la base de données :
```SQL
CREATE DATABASE SchoolDB;
```

## Mettre à jour la chaîne de connexion
Assurez-vous que la chaîne de connexion dans le fichier appsettings.json est correcte et correspond à votre configuration MySQL :

```json
"ConnectionStrings": {
  "DefaultConnection": "server=projetwebapiserver.mysql.database.azure.com;port=3306;database=SchoolDB;user=ProjetWebAPI;password=root666!;sslmode=required;"
}
```

### 3. Appliquer les migrations de la base de données
Une fois la base de données créée et la chaîne de connexion configurée, appliquez les migrations pour initialiser la base de données avec les tables nécessaires :
```cmd
dotnet ef database update
```

### 4. Lancer l'application

Vous pouvez maintenant lancer l'application. Assurez-vous d'étre dans le répertoire racine du projet, puis exécutez :

```cmd
dotnet run
```

L'API sera accessible à l'adresse suivante : https://localhost:7173.

### 5. Accéder à Swagger en local

Swagger est intégré pour la documentation et le test de l'API. Une fois l'application en cours d'exécution, ouvrez votre navigateur et accédez à :

```URL
https://localhost:7173/swagger/index.html
```

### 6. API déployée
Cette API est également déjà déployée sur Azure à l'adresse suivante : [API déployée](https://projetwebapi-romain-adrien-julien.azurewebsites.net/)
