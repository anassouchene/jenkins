pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                // Récupère le code source depuis le dépôt Git
                checkout scm
            }
        }
        stage('Restore') {
            steps {
                echo 'Restoring .NET dependencies...'
                bat 'dotnet restore ./GestionBibliotheque/GestionBibliotheque.sln'
            }
        }
        stage('Build') {
            steps {
                echo 'Building .NET project...'
                bat 'dotnet build ./GestionBibliotheque/GestionBibliotheque.sln --configuration Release'
            }
        }
        stage('Test') {
            steps {
                echo 'Running .NET tests...'
                bat 'dotnet test ./GestionBibliotheque/GestionBibliotheque.sln'
            }
        }
        stage('Publish') {
            steps {
                echo 'Publishing .NET application...'
                bat 'dotnet publish ./GestionBibliotheque/GestionBibliotheque.sln --configuration Release --output ./publish'
            }
        }
    }
    post {
        success {
            echo 'Build and deployment succeeded!'
        }
        failure {
            echo 'Build or deployment failed.'
        }
    }
}
