-- Utiliser la base de données appropriée
USE SchoolDB;

-- Insertion des données pour la table `Prof`
INSERT INTO Prof
    (Nom, Prenom, Matiere)
VALUES
    ('Dupont', 'Jean', 'Mathématiques'),
    ('Durand', 'Marie', 'Physique'),
    ('Lambert', 'Paul', 'Chimie'),
    ('Bernard', 'Sophie', 'Histoire');

-- Insertion des données pour la table `Etudiant`
INSERT INTO Etudiant
    (Nom, Prenom, Age)
VALUES
    ('Martin', 'Alice', 20),
    ('Petit', 'Olivier', 19),
    ('Robert', 'Claire', 22),
    ('Richard', 'Émile', 21);

-- Insertion des données pour la table `Cours`
INSERT INTO Cours
    (Titre, Description, ProfId)
VALUES
    ('Algèbre', 'Introduction aux concepts fondamentaux de l''algèbre', (SELECT Id
        FROM Prof
        WHERE Nom = 'Dupont' AND Prenom = 'Jean')),
    ('Mécanique classique', 'Étude des lois de Newton et leurs applications', (SELECT Id
        FROM Prof
        WHERE Nom = 'Durand' AND Prenom = 'Marie')),
    ('Chimie organique', 'Comprendre la structure, propriétés, compositions, réactions, et préparation des composés carbonés', (SELECT Id
        FROM Prof
        WHERE Nom = 'Lambert' AND Prenom = 'Paul')),
    ('Révolutions françaises', 'Analyse détaillée des révolutions françaises à travers l''histoire', (SELECT Id
        FROM Prof
        WHERE Nom = 'Bernard' AND Prenom = 'Sophie'));
