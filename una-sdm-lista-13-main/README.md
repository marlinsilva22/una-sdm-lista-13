# ✈️ AmericanAirlinesSkyApi

API desenvolvida para a disciplina de Sistemas Distribuídos e Mobile (UNA), simulando o gerenciamento de voos, aeronaves e reservas.

---

## 🚀 Como executar

```bash
dotnet run
```

Acesse:
http://localhost:5221/swagger

---

## 📌 Funcionalidades

* Cadastro de aeronaves
* Criação de voos com validação de disponibilidade
* Sistema de reservas com controle de overbooking
* Atualização de status de voo
* Consulta de destinos com agrupamento de voos

---

## ⚠️ Regras de Negócio

* Não permite criar voo com aeronave já em uso (**Conflict**)
* Não permite reservas acima da capacidade (**BadRequest**)
* Não permite alterar voo finalizado para "Em Voo"

---

## ⏳ Latência

A rota `/api/radar/proximos-destinos` simula atraso com `Thread.Sleep(2000)`, representando chamadas externas e impactando o tempo de resposta.

---

## 🧠 Concorrência

Para evitar conflitos em reservas simultâneas:

* Optimistic Concurrency (mais usada)
* Pessimistic Locking

---

## 📸 Evidências

Os testes realizados podem ser visualizados na pasta `/prints`.

---

## 👨‍💻 Autor

Bernardo Araújo
