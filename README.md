# smartcoin-net

Smartcoin C# .NET lib

## Instalação

### Instalação via repositório

Clone o código:

    $ git clone https://github.com/smartcoinpayments/smartcoin-net.git


## Documentação

Visite [smartcoin.com.br/api/](https://smartcoin.com.br/api/) para consultar documentação de referência.

## Exemplo de uso:

```csharp

# Setup Account keys

using Smartcoin;
using Smartcoin.Entities;
SmartcoinConfiguration.SetKeys('pk_test_407d1f51a61756',
	'sk_test_86e4486a0078b2'); # Replace keys by your account keys

# Create a Token with card information

var token = new Token();
token.Card = new Card();
token.Card.Number = 4242424242424242; 
token.Card.ExpMonth = 12;
token.Card.ExpYear = 2018;
token.Card.Cvc = 123;
token.Card.Name = "Luke Skywalker";

t.Create();

# Create Charge with token as card param

var c = new Charge();
c.Type = "credit_card";
c.Installment = 1;
c.Amount = 100;
c.Currency = "brl";
c.Description = "Smartcoin charge test for example@test.com";
c.CardToken = token;

c.Create();

# Create Bank Slip Charge

var c = new Charge();
c.Type = "bank_slip";
c.Amount = 100;
c.Currency = "brl";
c.Description = "Smartcoin charge test for example@test.com";

c.Create();

# Create Subscription

var plan = new Plan();
plan.Id = "test-plan";
plan.Amount = 1000;
plan.Currency = "brl";
plan.Interval  = "week";
plan.IntervalCount = 2;
plan.Name = "Plan Name";
plan.TrialPeriodDays = 15;

var pl = plan.Create();

var customer = new Customer();
customer.Email = "customer@email.com";
var cus = customer.Create();

var subscription = new Subscription();
subscription.Customer = cus.Id;
subscription.Plan = pl;
subscription.TrialEnd = 7;
subscription.Quantity = 2;

subscription.Create();

```

## Teste

Para instalar a suíte de testes, siga as intruções em:

	http://docs.nuget.org/consume/installing-nuget

Para executar a suíte de testes:

	Execute NuGet.exe

## Autor

Originally by [Marcio Jorge](https://github.com/mnfjorge).

Colaborador(es):
    [Ricardo Caldeira](https://github.com/ricocaldeira)