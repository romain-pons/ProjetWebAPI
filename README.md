# SchoolDB API

## Contributeurs
Julien LAY - [GitHub](https://github.com/JulienLAY)
Romain PONS - [GitHub](https://github.com/romain-pons)
Adrien AUTEF - [GitHub](https://github.com/AdrienAUTEF)

## Description

SchoolDB API est un projet d'API RESTful d�velopp� en ASP.NET Core. Il permet la gestion d'une base de donn�es de l'�cole, incluant des entit�s telles que les �tudiants, les professeurs et les cours. Le projet inclut �galement un front-end simple, d�velopp� en HTML, CSS et JavaScript, situ� dans le dossier `wwwroot`.

## Pr�requis

Avant de lancer le projet, assurez-vous d'avoir install� les �l�ments suivants :

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/) (facultatif mais recommand� pour l'�dition et le debug)
- Un client MySQL (tel que MySQL Workbench) pour g�rer la base de donn�es.

## Structure du projet
Controllers/ : Contient les contr�leurs de l'API qui g�rent les diff�rentes routes pour les entit�s Etudiant, Prof, et Cour.
Models/ : D�finit les mod�les de donn�es utilis�s dans l'API.
Data/ : Contient le contexte de base de donn�es (AppDbContext) et les services qui interagissent avec la base de donn�es.
wwwroot/ : Contient les fichiers du front-end, incluant HTML, CSS, et JavaScript.
appsettings.json : Contient la configuration de l'application, notamment la cha�ne de connexion � la base de donn�es.

## Installation

### 1. Clonez le d�p�t

Clonez le d�p�t GitHub du projet � l'aide de la commande suivante :

```bash
git clone https://github.com/romain-pons/ProjetWebAPI.git
cd ProjetWebAPI
```

### 2. Configurer la base de donn�es MySQL

Vous devez configurer une base de donn�es MySQL pour le projet. Vous pouvez soit utiliser une base de donn�es MySQL locale, soit une base de donn�es MySQL h�berg�e sur le cloud, comme celle mentionn�e dans appsettings.json.

Cr�er la base de donn�es
Si vous n'avez pas encore de base de donn�es, connectez-vous � votre serveur MySQL et ex�cutez la commande suivante pour cr�er la base de donn�es :
```SQL
CREATE DATABASE SchoolDB;
```

## Mettre � jour la cha�ne de connexion
Assurez-vous que la cha�ne de connexion dans le fichier appsettings.json est correcte et correspond � votre configuration MySQL :

```json
"ConnectionStrings": {
  "DefaultConnection": "server=<votre_serveur>;port=3306;database=SchoolDB;user=<votre_utilisateur>;password=<votre_mot_de_passe>;sslmode=required;"
}
```

Remplacez <votre_serveur>, <votre_utilisateur>, et <votre_mot_de_passe> par les informations appropri�es.

### 3. Appliquer les migrations de la base de donn�es
Une fois la base de donn�es cr��e et la cha�ne de connexion configur�e, appliquez les migrations pour initialiser la base de donn�es avec les tables n�cessaires :
```cmd
dotnet ef database update
```

### 4. Lancer l'application

Vous pouvez maintenant lancer l'application. Assurez-vous d'�tre dans le r�pertoire racine du projet, puis ex�cutez :

```cmd
dotnet run
```

L'API sera accessible � l'adresse suivante : https://localhost:7173.

### 5. Acc�der � Swagger

Swagger est int�gr� pour la documentation et le test de l'API. Une fois l'application en cours d'ex�cution, ouvrez votre navigateur et acc�dez � :

```URL
https://localhost:7173/swagger/index.html
```

### 6. Acc�der au Front-end

A MODIFIER

Le projet contient un front-end simple dans le dossier wwwroot. Vous pouvez y acc�der en ouvrant directement :

```URL
https://localhost:7173/index.html
```