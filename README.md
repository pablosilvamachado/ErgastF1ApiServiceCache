# Formula 1 API 

Bem-vindo ao repositório da **Formula 1 API**, uma ferramenta pensada para desenvolvedores e entusiastas da Fórmula 1 que buscam explorar dados históricos de maneira simples e organizada. Este projeto tem como foco o estudo e a implementação de conceitos de cache com Redis, bem como a aplicação de padrões de arquitetura limpa (Clean Architecture), SOLID e DDD (Domain-Driven Design).

## Objetivo do Projeto

Este repositório é fruto de um estudo aprofundado em técnicas de otimização e arquitetura de sistemas, em especial o uso de **Redis** para caching. A ideia é explorar como podemos melhorar o desempenho e a escalabilidade de APIs, evitando chamadas repetitivas e desnecessárias a serviços externos.

Neste momento, a API implementa apenas o endpoint relacionado às **temporadas da Fórmula 1** (**Seasons**). Planejamos expandir para outros endpoints, como **corridas**, **pilotos** e **equipes**, em futuras atualizações.

---

## Funcionalidades

- **Endpoint Atual:**
  - `/seasons`: Retorna uma lista de temporadas históricas da Fórmula 1.
- **Cache com Redis:**
  - Redução de chamadas repetitivas ao serviço externo, armazenando os resultados em cache por uma hora.
- **Clean Architecture:**
  - A lógica de negócio é separada em camadas, garantindo manutenção e expansão fáceis.

---

## Tecnologias Utilizadas

- **.NET 8**
- **Redis**
- **Clean Architecture**
- **SOLID**
- **DDD (Domain-Driven Design)**
- **API Ergast** (Dados da Fórmula 1)

---

## Como Instalar e Executar o Projeto

### Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Redis](https://redis.io/download)
- Ferramenta de linha de comando para executar o Redis (ex.: `docker`, `redis-cli`).

### Passo a Passo

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/pablosilvamachado/ErgastF1ApiServiceCache.git
   cd ErgastF1ApiServiceCache
   ```

2. **Configure o Redis:**
   Certifique-se de que o Redis está instalado e em execução.
   Caso use o Docker, você pode iniciar o Redis com o comando:
   ```bash
   docker run --name redis -d -p 6379:6379 redis
   ```

3. **Configure o projeto:**
   No arquivo `appsettings.json`, atualize as configurações do Redis caso necessário:
   ```json
   "Redis": {
     "Connection": "localhost:6379"
   }
   ```

4. **Execute o projeto:**
   ```bash
   dotnet run
   ```

5. **Teste o endpoint:**
   Use um cliente HTTP (como [Postman](https://www.postman.com/) ou [Insomnia](https://insomnia.rest/)) para acessar o endpoint:
   ```http
   GET http://localhost:5000/seasons
   ```

---

## Exemplos de Resposta

### Sucesso

```json
{
  "MRData": {
    "SeasonTable": {
      "Seasons": [
        {
          "season": "1950",
          "url": "http://en.wikipedia.org/wiki/1950_Formula_One_season"
        },
        {
          "season": "1951",
          "url": "http://en.wikipedia.org/wiki/1951_Formula_One_season"
        }
      ]
    }
  }
}
```

---

## Próximos Passos

Estamos comprometidos em expandir esta API para incluir:

1. **Corridas (Races)**
2. **Pilotos (Drivers)**
3. **Equipes (Constructors)**
4. **Classificação (Standings)**

Cada novo endpoint será implementado utilizando os mesmos padrões de qualidade e boas práticas aplicados na primeira versão.

---

## Contribuição

Se você é um desenvolvedor interessado em Fórmula 1 ou em boas práticas de desenvolvimento, fique à vontade para contribuir!

- Faça um fork do projeto.
- Crie uma branch para sua funcionalidade ou correção: `git checkout -b minha-feature`.
- Submeta um pull request explicando as mudanças.

---

## Licença

Este projeto é distribuído sob a licença MIT. Consulte o arquivo [LICENSE](LICENSE) para mais detalhes.

---

## Agradecimentos

- Aos entusiastas da Fórmula 1 pela paixão pelo esporte.
- Ao time por trás da [Ergast API](https://ergast.com/mrd/) por fornecer os dados.
- À comunidade de desenvolvedores por compartilhar conhecimento e boas práticas.

