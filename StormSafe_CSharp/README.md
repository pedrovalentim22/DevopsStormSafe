# 🌊 StormSafe - Sistema Inteligente de Evacuação em Enchentes

> Projeto acadêmico da disciplina **Advanced Business Development with .NET**  
> FIAP - Global Solution 2025

---

## 📌 Descrição

StormSafe é uma plataforma web desenvolvida em .NET 8 que monitora sensores de nível de água instalados em rios e envia alertas para rotas seguras de evacuação em casos de enchente. O sistema oferece:

- Gerenciamento de **rios**, **sensores**, **usuários** e **rotas de evacuação**
- Interface administrativa com **Razor Pages** e **TagHelpers**
- API RESTful com **Swagger**
- Banco de dados relacional via **Entity Framework Core**

---

## 🧱 Diagrama Entidade-Relacionamento

📊 Relacionamento 1:N entre Rio e Sensor.  
🧑 Usuário cadastra rotas e visualiza alertas.

```
[USUARIO]---(1:N)---[ALERTA]
    |
[ROTA EVACUAÇÃO]

[RIO]---(1:N)---[SENSOR]
```

> Modelo implementado com base em boas práticas e normalização até 3FN.

---

## 🏗️ Desenvolvimento

### 🔧 Tecnologias utilizadas

- ASP.NET Core 8
- Entity Framework Core + Migrations
- SQL Server LocalDB
- Razor Pages + TagHelpers
- Swagger para documentação
- Visual Studio 2022

### 🧭 Organização de Pastas

```
StormSafe/
├── Controllers/
├── Models/
├── Data/              # DbContext e Migrations
├── Views/             # Razor Pages
├── wwwroot/           # Assets (CSS, JS)
└── Program.cs
```

### ⚙️ Execução local

```bash
# Clonar o projeto
git clone https://github.com/SEU_USUARIO/StormSafe.git

# Navegar até a pasta
cd StormSafe

# Restaurar pacotes
dotnet restore

# Atualizar banco de dados
dotnet ef database update

# Executar
dotnet run
```

Acesse:
- Interface Web: https://localhost:5001/Rios
- Swagger: https://localhost:5001/swagger

---

## 🧪 Testes

> A aplicação foi testada manualmente via Swagger, Navegador e banco de dados.

### Casos testados

| Caso                           | Resultado Esperado                  | Status |
|--------------------------------|-------------------------------------|--------|
| Criar novo Rio                 | Rio salvo no banco                  | ✅     |
| Cadastrar Sensor atrelado a Rio| Sensor aparece vinculado no Swagger| ✅     |
| Criar Rota e Listar            | Rota visível em Razor Pages         | ✅     |
| Criar Alerta para Sensor       | Alerta salvo com sucesso            | ✅     |
| Validação de campos obrigatórios| Retorno 400 no Swagger             | ✅     |

---

## 🧪 Endpoints Principais

| Método | Rota                          | Descrição                           |
|--------|-------------------------------|--------------------------------------|
| GET    | `/api/rios`                   | Listar rios cadastrados              |
| POST   | `/api/sensores`               | Criar novo sensor                    |
| GET    | `/api/rios/{id}/sensores`     | Listar sensores de um rio específico|
| GET    | `/api/rotas`                  | Buscar todas as rotas               |
| POST   | `/api/alertas`                | Registrar novo alerta               |

---

## 👨‍💻 Integrantes

| Nome                  | RM        |
|-----------------------|-----------|
| Miguel Barros Ramos   | RM556652  |
| Pedro Valentim Merise | RM556826  |

---

## 📹 Vídeos da Entrega

| Tipo         | Link                                  |
|--------------|---------------------------------------|
| 🎬 Pitch      | [ASSISTIR AQUI](https://youtu.be/PITCH) |
| 📽️ Demonstração | [ASSISTIR AQUI](https://youtu.be/DEMO)  |

---

## 📄 Licença

Projeto acadêmico. Uso restrito a fins educacionais (FIAP - 2025).
