# API-Challenge
🚀 API REST para movimentação de conta bancária

### 📋 Pré-requisitositos
Para poder executar esse projeto será necessário fazer o download do banco de dados [MySQL](https://dev.mysql.com/downloads/installer/) - a versão utilizada é 8.0.25.0
```
 Senha: B9zptUl64tu7@2021
 User : root
```

### 🔧 Instalação
Após abrir o projeto no [Microsoft Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/), navegue até o console do Gerenciador de Pacotes (Ferramentas > Gerenciador de Pacotes NuGet > Console do Gerenciador de Pacotes) e execute o comando:
```
Add-Migration Initial
```

Após esse procedimento, execute:
```
Update-Database
```

## ⚙️ Executando os testes
Ao iniciar a depuração do projeto será carregado uma página html com o dashboard do Swagger.
As chamadas são do tipo:

##  POST
```
  /api/Conta
  Deve-se enviar um arquivo JSON no body da requisição com as informações da conta (número e saldo inicial)
  {
    "numero": 0,
    "saldo": 0
  }
```

##  GET
```
  /api/Conta
  Retorna um JSON com todas as contas cadastradas no banco de dados
  
  /api/Conta/{numeroConta}
  Retorna um JSON com os dados da conta informada
  
  /api/Conta/saldo/{numeroConta}
  Retorna o saldo da conta informada
```

##  PUT
```
  /api/Conta/sacar/{conta}/{valor}
  Deverá constar na requisição o número da conta e o valor que deseja sacar
  
  /api/Conta/depositar/{conta}/{valor}  
  Deverá constar na requisição o número da conta e o valor que deseja depositar
```


## ✒️ Autor
* **André Luiz**
