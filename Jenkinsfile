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
                bat 'dotnet restore ./GestionBibliotheque/GestionBiblio.sln'
            }
        }
        stage('Build') {
            steps {
                echo 'Building .NET project...'
                bat 'dotnet build ./GestionBibliotheque/GestionBiblio.sln --configuration Release'
            }
        }
        stage('Test') {
            steps {
                echo 'Running .NET tests...'
                bat 'dotnet test ./GestionBibliotheque/GestionBiblio.sln'
            }
        }
        stage('Publish') {
            steps {
                echo 'Publishing .NET application...'
                bat 'dotnet publish ./GestionBibliotheque/GestionBiblio.sln --configuration Release --output ./publish'
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
