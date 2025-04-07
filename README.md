# 💼 BarberPro.Api

Sistema de Barbearia feito com **.NET 8**, **LINQ To DB** e arquitetura organizada por domínios e responsabilidades.

---

## 🧠 Visão Geral

Projeto dividido em camadas:

- **Domain**: Entidades, interfaces e DTOs.
- **Application**: Regras de negócio com os services.
- **Infra**: Persistência com LINQ To DB e repositórios.
- **API**: Configuração e execução da aplicação.

---

## 📁 Estrutura de Pastas

```
BarberPro.Api/
├── BarberPro.Domain/
│   ├── Entities/
│   ├── DTOs/
│   └── Interfaces/
├── BarberPro.Application/
│   ├── Interfaces/
│   └── Services/
├── BarberPro.Infra/
│   ├── Data/
│   └── Repositories/
├── Configuration/
│   ├── Linq2DbConfig.cs
│   ├── DependencyInjection.cs
│   └── DatabaseInitializer.cs
├── Program.cs
└── init_barberpro_schema.sql
```

---

## 🗃️ Tecnologias

- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [LINQ To DB](https://linq2db.github.io/)
- [SQLite](https://www.sqlite.org/index.html)
- Swagger para documentação da API

---

## 🧪 Executando o Projeto

1. **Clone o repositório**

```bash
git clone https://github.com/seuusuario/BarberPro.Api.git
cd BarberPro.Api
```

2. **Configure o `appsettings.json`**

Adicione a string de conexão:

```json
"ConnectionStrings": {
  "BarberProDB": "Data Source=barberpro.db"
}
```

3. **Rode o projeto**

```bash
dotnet run
```

> A aplicação irá inicializar o banco de dados SQLite automaticamente com base no script `init_barberpro_schema.sql`.

---

## 📌 Funcionalidades

### Usuários

- Cadastro, listagem, edição e remoção
- Identificação se é cliente ou barbeiro

### Agendamentos

- Agendar com um barbeiro disponível
- Listar agendamentos por usuário
- Verificar horários disponíveis por barbeiro e data

---

## 🛠️ DI e Inicialização

Toda a injeção de dependência é feita com `AddBarberProServices()` no `Program.cs`.

Inicialização automática do banco com:

```csharp
await DatabaseInitializer.InicializarAsync(builder.Configuration.GetConnectionString("BarberProDB"));
```

---

## 📚 Swagger

Acesse `https://localhost:{porta}/swagger` para testar os endpoints da API visualmente.

---

## 🚀 Futuras melhorias

- Autenticação JWT
- Interface web com Blazor ou Angular
- Integração com e-mail e notificações

---

## 📃 Licença

Projeto feito para fins de estudo. Sinta-se livre para usar, modificar e contribuir! 😉
