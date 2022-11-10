# Star Wars

Api para informações da franquia Star Wars.

## Stacks
Essa api foi feita na linguagem .Net Core versão 6, banco de dados SQL, os dois com suporte docker.

## Requerido
Máquina com docker instalado

## Instalação
Para rodar o projeto, em linha de comando no diretório do mesmo, execute

```bash
 docker-compose up -d
```

## Execução
O projeto estará rodando na porta 80 da sua máquina. 
### - Endpoints disponíveis:

Criar planeta -> Post -> http://localhost:80/planet/create/{idDoPlaneta}

Listar planeta -> Get -> http://localhost:80/planet/

Consultar planeta por Id -> Get -> http://localhost:80/planet/{idDoPlaneta}

Consultar planeta por nome -> Get -> http://localhost:80/planet/name/{nomeDoPlaneta}

Deletar planeta -> Delete -> http://localhost:80/planet/{idDoPlaneta}