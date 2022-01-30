
# Design Pattern Visitor

## Problema Proposto
O modelo de problema proposto é de uma loja de carros.

Pense que temos uma separação de alguns tipos de produtos (carros, motos e monociclos). Esses tipos 
são separados assim pois nossa loja de carros vai atuar em 3 estados brasileiros distintos e cada um deles
cobra uma taxa especifica de imposto, outro ponto é que nossos vendedores tem comissões especificas em cada
estado.

## Reflexão
Preciso que você pense em formas de resolver esse problema respeitando os principios SOLID e com o melhor
pattern possivel. Antes vou apresentar as classes.

[![](https://mermaid.ink/img/eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIElJbXBvc3Rve1xuIDw8aW50ZXJmYWNlPj5cbkNhbGN1bGFySW1wb3N0byhDYXJybyBjYXJybykgZGVjaW1hbFxufVxuXG5jbGFzcyBJbXBvc3RvQ2Fycm9TYW9QYXVsb1xuXG5jbGFzcyBJbXBvc3RvTW90b1Nhb1BhdWxvXG5cbmNsYXNzIEltcG9zdG9Nb25vY2ljbG9TYW9QYXVsb1xuXG5JSW1wb3N0byA8fC0tIEltcG9zdG9DYXJyb1Nhb1BhdWxvXG5JSW1wb3N0byA8fC0tIEltcG9zdG9Nb3RvU2FvUGF1bG9cbklJbXBvc3RvIDx8LS0gSW1wb3N0b01vbm9jaWNsb1Nhb1BhdWxvXG4gICIsIm1lcm1haWQiOnsidGhlbWUiOiJkYXJrIn0sInVwZGF0ZUVkaXRvciI6ZmFsc2UsImF1dG9TeW5jIjp0cnVlLCJ1cGRhdGVEaWFncmFtIjpmYWxzZX0)](https://mermaid-js.github.io/mermaid-live-editor/edit#eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIElJbXBvc3Rve1xuIDw8aW50ZXJmYWNlPj5cbkNhbGN1bGFySW1wb3N0byhDYXJybyBjYXJybykgZGVjaW1hbFxufVxuXG5jbGFzcyBJbXBvc3RvQ2Fycm9TYW9QYXVsb1xuXG5jbGFzcyBJbXBvc3RvTW90b1Nhb1BhdWxvXG5cbmNsYXNzIEltcG9zdG9Nb25vY2ljbG9TYW9QYXVsb1xuXG5JSW1wb3N0byA8fC0tIEltcG9zdG9DYXJyb1Nhb1BhdWxvXG5JSW1wb3N0byA8fC0tIEltcG9zdG9Nb3RvU2FvUGF1bG9cbklJbXBvc3RvIDx8LS0gSW1wb3N0b01vbm9jaWNsb1Nhb1BhdWxvXG4gICIsIm1lcm1haWQiOiJ7XG4gIFwidGhlbWVcIjogXCJkYXJrXCJcbn0iLCJ1cGRhdGVFZGl0b3IiOmZhbHNlLCJhdXRvU3luYyI6dHJ1ZSwidXBkYXRlRGlhZ3JhbSI6ZmFsc2V9)

```mermaid
    classDiagram
    
    class Estados{
        <<enumeration>>
        RIO_DE_JANEIRO,
        SAO_PAULO,
        MATO_GROSSO
    }
    
    class Vendedor{
        +String Nome
        +Etados Estado
        +Decimal Salario
        
        +Vender(Carro carro) decimal
    }

    class Veiculo{
        +String Nome
        +Decimal Valor
    }
    
    class Carro
    class Moto
    class Monociclo
    
    Veiculo <|-- Carro
    Veiculo <|-- Moto
    Veiculo <|-- Monociclo

```

### Solução Ruim
Se você pensou em implementar varios _ifs_ na classe que define os impostos e comissão,
você pensou na pior solução hshshshs, mas relaxa estou aqui para passar um pouco 
do conhecimento que eu tenho para te ajudar a melhorar.

```csharp
class Vendedor{
    public string Nome {get; set;}
    public Estados Estado {get; set;}
    public decimal Salario {get; set;}
    
    public decimal Vender(Veiculo carro){
        decimal comissao = 0.0;
        decimal imposto = 0.0;
        
        if(Estado == Estados.SAO_PAULO){
            if(veiculo.GetType() == typeof(Carro)){
               // implementar comissão do Carro em São Paulo
            }
            else if(veiculo.GetType() == typeof(Moto)){
               // implementar comissão de Moto em São Paulo
            }
            else if(veiculo.GetType() == typeof(Monociclo)){
               // implementar comissão de Monociclo em São Paulo
            }
        }
        else if(Estado == Estados.RIO_DE_JANEIRO){
            if(veiculo.GetType() == typeof(Carro)){
               // implementar comissão do Carro no Rio de Janeiro
            }
            else if(veiculo.GetType() == typeof(Moto)){
               // implementar comissão de Moto no Rio de Janeiro
            }
            else if(veiculo.GetType() == typeof(Monociclo)){
               // implementar comissão de Monociclo no Rio de Janeiro
            }
        }
        else if(Estado == Estados.MATO_GROSSO){
            if(veiculo.GetType() == typeof(Carro)){
               // implementar comissão do Carro em Mato Grosso
            }
            else if(veiculo.GetType() == typeof(Moto)){
               // implementar comissão de Moto em Mato Grosso
            }
            else if(veiculo.GetType() == typeof(Monociclo)){
               // implementar comissão de Monociclo em Mato Grosso
            }
        }
        
        return Salario - comissao + imposto;
    }
}
```

### Solução Mediana
Utilizar o design pattern Strategy para criar estratégias de calculo de imposto e comissão para cada estado.
Ela é mediana pois podemos tirar aquela porrada de _ifs_ da solução anterior fazendo uma injeção de dependencia no método _Vender()_,
porém vamos criar muitas estratégias se quisermos fazer isso de maneira correta, pois perceba que cada estratégia tem 3 subestratégias,
ou seja, cada estado vai tratar de 3 categorias distintas de carros portando generalizanado teriamos n * m estratégias, um verdadeiro
caos quando o sistema crescer. Vou fazer o diagrama de UML e você vai ver a representação gráfica do que falei.

### IImposto
#### São Paulo
[![](https://mermaid.ink/img/eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIElJbXBvc3Rve1xuIDw8aW50ZXJmYWNlPj5cbkNhbGN1bGFySW1wb3N0byhDYXJybyBjYXJybykgZGVjaW1hbFxufVxuXG5jbGFzcyBJbXBvc3RvQ2Fycm9TYW9QYXVsb1xuXG5jbGFzcyBJbXBvc3RvTW90b1Nhb1BhdWxvXG5cbmNsYXNzIEltcG9zdG9Nb25vY2ljbG9TYW9QYXVsb1xuXG5JSW1wb3N0byA8fC0tIEltcG9zdG9DYXJyb1Nhb1BhdWxvXG5JSW1wb3N0byA8fC0tIEltcG9zdG9Nb3RvU2FvUGF1bG9cbklJbXBvc3RvIDx8LS0gSW1wb3N0b01vbm9jaWNsb1Nhb1BhdWxvXG4gICIsIm1lcm1haWQiOnsidGhlbWUiOiJkYXJrIn0sInVwZGF0ZUVkaXRvciI6ZmFsc2UsImF1dG9TeW5jIjp0cnVlLCJ1cGRhdGVEaWFncmFtIjpmYWxzZX0)](https://mermaid-js.github.io/mermaid-live-editor/edit#eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIElJbXBvc3Rve1xuIDw8aW50ZXJmYWNlPj5cbkNhbGN1bGFySW1wb3N0byhDYXJybyBjYXJybykgZGVjaW1hbFxufVxuXG5jbGFzcyBJbXBvc3RvQ2Fycm9TYW9QYXVsb1xuXG5jbGFzcyBJbXBvc3RvTW90b1Nhb1BhdWxvXG5cbmNsYXNzIEltcG9zdG9Nb25vY2ljbG9TYW9QYXVsb1xuXG5JSW1wb3N0byA8fC0tIEltcG9zdG9DYXJyb1Nhb1BhdWxvXG5JSW1wb3N0byA8fC0tIEltcG9zdG9Nb3RvU2FvUGF1bG9cbklJbXBvc3RvIDx8LS0gSW1wb3N0b01vbm9jaWNsb1Nhb1BhdWxvXG4gICIsIm1lcm1haWQiOiJ7XG4gIFwidGhlbWVcIjogXCJkYXJrXCJcbn0iLCJ1cGRhdGVFZGl0b3IiOmZhbHNlLCJhdXRvU3luYyI6dHJ1ZSwidXBkYXRlRGlhZ3JhbSI6ZmFsc2V9)

```mermaid
classDiagram

class IImposto{
 <<interface>>
CalcularImposto(Carro carro) decimal
}

class ImpostoCarroSaoPaulo

class ImpostoMotoSaoPaulo

class ImpostoMonocicloSaoPaulo

IImposto <|-- ImpostoCarroSaoPaulo
IImposto <|-- ImpostoMotoSaoPaulo
IImposto <|-- ImpostoMonocicloSaoPaulo

```
#### Rio de Janeiro
[![](https://mermaid.ink/img/eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIElJbXBvc3Rve1xuIDw8aW50ZXJmYWNlPj5cbkNhbGN1bGFySW1wb3N0byhDYXJybyBjYXJybykgZGVjaW1hbFxufVxuXG5jbGFzcyBJbXBvc3RvQ2Fycm9SaW9EZUphbmVpcm9cblxuY2xhc3MgSW1wb3N0b01vdG9SaW9EZUphbmVpcm9cblxuY2xhc3MgSW1wb3N0b01vbm9jaWNsb1Jpb0RlSmFuZWlyb1xuXG5JSW1wb3N0byA8fC0tIEltcG9zdG9DYXJyb1Jpb0RlSmFuZWlyb1xuSUltcG9zdG8gPHwtLSBJbXBvc3RvTW90b1Jpb0RlSmFuZWlyb1xuSUltcG9zdG8gPHwtLSBJbXBvc3RvTW9ub2NpY2xvUmlvRGVKYW5laXJvXG4gICIsIm1lcm1haWQiOnsidGhlbWUiOiJkYXJrIn0sInVwZGF0ZUVkaXRvciI6ZmFsc2UsImF1dG9TeW5jIjp0cnVlLCJ1cGRhdGVEaWFncmFtIjpmYWxzZX0)](https://mermaid-js.github.io/mermaid-live-editor/edit#eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIElJbXBvc3Rve1xuIDw8aW50ZXJmYWNlPj5cbkNhbGN1bGFySW1wb3N0byhDYXJybyBjYXJybykgZGVjaW1hbFxufVxuXG5jbGFzcyBJbXBvc3RvQ2Fycm9SaW9EZUphbmVpcm9cblxuY2xhc3MgSW1wb3N0b01vdG9SaW9EZUphbmVpcm9cblxuY2xhc3MgSW1wb3N0b01vbm9jaWNsb1Jpb0RlSmFuZWlyb1xuXG5JSW1wb3N0byA8fC0tIEltcG9zdG9DYXJyb1Jpb0RlSmFuZWlyb1xuSUltcG9zdG8gPHwtLSBJbXBvc3RvTW90b1Jpb0RlSmFuZWlyb1xuSUltcG9zdG8gPHwtLSBJbXBvc3RvTW9ub2NpY2xvUmlvRGVKYW5laXJvXG4gICIsIm1lcm1haWQiOiJ7XG4gIFwidGhlbWVcIjogXCJkYXJrXCJcbn0iLCJ1cGRhdGVFZGl0b3IiOmZhbHNlLCJhdXRvU3luYyI6dHJ1ZSwidXBkYXRlRGlhZ3JhbSI6ZmFsc2V9)

```mermaid
classDiagram

class IImposto{
 <<interface>>
CalcularImposto(Carro carro) decimal
}

class ImpostoCarroRioDeJaneiro

class ImpostoMotoRioDeJaneiro

class ImpostoMonocicloRioDeJaneiro

IImposto <|-- ImpostoCarroRioDeJaneiro
IImposto <|-- ImpostoMotoRioDeJaneiro
IImposto <|-- ImpostoMonocicloRioDeJaneiro

```
#### Mato Grosso
[![](https://mermaid.ink/img/eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIElJbXBvc3Rve1xuIDw8aW50ZXJmYWNlPj5cbkNhbGN1bGFySW1wb3N0byhDYXJybyBjYXJybykgZGVjaW1hbFxufVxuXG5jbGFzcyBJbXBvc3RvQ2Fycm9NYXRvR3Jvc3NvXG5cbmNsYXNzIEltcG9zdG9Nb3RvTWF0b0dyb3Nzb1xuXG5jbGFzcyBJbXBvc3RvTW9ub2NpY2xvTWF0b0dyb3Nzb1xuXG5JSW1wb3N0byA8fC0tIEltcG9zdG9DYXJyb01hdG9Hcm9zc29cbklJbXBvc3RvIDx8LS0gSW1wb3N0b01vdG9NYXRvR3Jvc3NvXG5JSW1wb3N0byA8fC0tIEltcG9zdG9Nb25vY2ljbG9NYXRvR3Jvc3NvXG5cbiAgIiwibWVybWFpZCI6eyJ0aGVtZSI6ImRhcmsifSwidXBkYXRlRWRpdG9yIjpmYWxzZSwiYXV0b1N5bmMiOnRydWUsInVwZGF0ZURpYWdyYW0iOmZhbHNlfQ)](https://mermaid-js.github.io/mermaid-live-editor/edit#eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIElJbXBvc3Rve1xuIDw8aW50ZXJmYWNlPj5cbkNhbGN1bGFySW1wb3N0byhDYXJybyBjYXJybykgZGVjaW1hbFxufVxuXG5jbGFzcyBJbXBvc3RvQ2Fycm9NYXRvR3Jvc3NvXG5cbmNsYXNzIEltcG9zdG9Nb3RvTWF0b0dyb3Nzb1xuXG5jbGFzcyBJbXBvc3RvTW9ub2NpY2xvTWF0b0dyb3Nzb1xuXG5JSW1wb3N0byA8fC0tIEltcG9zdG9DYXJyb01hdG9Hcm9zc29cbklJbXBvc3RvIDx8LS0gSW1wb3N0b01vdG9NYXRvR3Jvc3NvXG5JSW1wb3N0byA8fC0tIEltcG9zdG9Nb25vY2ljbG9NYXRvR3Jvc3NvXG5cbiAgIiwibWVybWFpZCI6IntcbiAgXCJ0aGVtZVwiOiBcImRhcmtcIlxufSIsInVwZGF0ZUVkaXRvciI6ZmFsc2UsImF1dG9TeW5jIjp0cnVlLCJ1cGRhdGVEaWFncmFtIjpmYWxzZX0)

```mermaid
classDiagram

class IImposto{
 <<interface>>
CalcularImposto(Carro carro) decimal
}

class ImpostoCarroMatoGrosso

class ImpostoMotoMatoGrosso

class ImpostoMonocicloMatoGrosso

IImposto <|-- ImpostoCarroMatoGrosso
IImposto <|-- ImpostoMotoMatoGrosso
IImposto <|-- ImpostoMonocicloMatoGrosso

```

### IComissao
#### São Paulo
[![](https://mermaid.ink/img/eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIElDb21pc3Nhb3tcbiA8PGludGVyZmFjZT4-XG5DYWxjdWxhckNvbWlzc2FvKENhcnJvIGNhcnJvKSBkZWNpbWFsXG59XG5cbmNsYXNzIENvbWlzc2FvQ2Fycm9TYW9QYXVsb1xuXG5jbGFzcyBDb21pc3Nhb01vdG9TYW9QYXVsb1xuXG5jbGFzcyBDb21pc3Nhb01vbm9jaWNsb1Nhb1BhdWxvXG5cbklDb21pc3NhbyA8fC0tQ29taXNzYW9DYXJyb1Nhb1BhdWxvXG5JQ29taXNzYW8gPHwtLUNvbWlzc2FvTW90b1Nhb1BhdWxvXG5JQ29taXNzYW8gPHwtLUNvbWlzc2FvTW9ub2NpY2xvU2FvUGF1bG9cblxuICAiLCJtZXJtYWlkIjp7InRoZW1lIjoiZGFyayJ9LCJ1cGRhdGVFZGl0b3IiOmZhbHNlLCJhdXRvU3luYyI6dHJ1ZSwidXBkYXRlRGlhZ3JhbSI6ZmFsc2V9)](https://mermaid-js.github.io/mermaid-live-editor/edit#eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIElDb21pc3Nhb3tcbiA8PGludGVyZmFjZT4-XG5DYWxjdWxhckNvbWlzc2FvKENhcnJvIGNhcnJvKSBkZWNpbWFsXG59XG5cbmNsYXNzIENvbWlzc2FvQ2Fycm9TYW9QYXVsb1xuXG5jbGFzcyBDb21pc3Nhb01vdG9TYW9QYXVsb1xuXG5jbGFzcyBDb21pc3Nhb01vbm9jaWNsb1Nhb1BhdWxvXG5cbklDb21pc3NhbyA8fC0tQ29taXNzYW9DYXJyb1Nhb1BhdWxvXG5JQ29taXNzYW8gPHwtLUNvbWlzc2FvTW90b1Nhb1BhdWxvXG5JQ29taXNzYW8gPHwtLUNvbWlzc2FvTW9ub2NpY2xvU2FvUGF1bG9cblxuICAiLCJtZXJtYWlkIjoie1xuICBcInRoZW1lXCI6IFwiZGFya1wiXG59IiwidXBkYXRlRWRpdG9yIjpmYWxzZSwiYXV0b1N5bmMiOnRydWUsInVwZGF0ZURpYWdyYW0iOmZhbHNlfQ)

```mermaid
classDiagram

class IComissao{
 <<interface>>
CalcularComissao(Carro carro) decimal
}

class ComissaoCarroSaoPaulo

class ComissaoMotoSaoPaulo

class ComissaoMonocicloSaoPaulo

IComissao <|--ComissaoCarroSaoPaulo
IComissao <|--ComissaoMotoSaoPaulo
IComissao <|--ComissaoMonocicloSaoPaulo

```

#### Rio de Janeiro
[![](https://mermaid.ink/img/eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIElDb21pc3Nhb3tcbiA8PGludGVyZmFjZT4-XG5DYWxjdWxhckNvbWlzc2FvKENhcnJvIGNhcnJvKSBkZWNpbWFsXG59XG5cbmNsYXNzIENvbWlzc2FvQ2Fycm9SaW9EZUphbmVpcm9cblxuY2xhc3MgQ29taXNzYW9Nb3RvUmlvRGVKYW5laXJvXG5cbmNsYXNzIENvbWlzc2FvTW9ub2NpY2xvUmlvRGVKYW5laXJvXG5cbklDb21pc3NhbyA8fC0tIENvbWlzc2FvQ2Fycm9SaW9EZUphbmVpcm9cbklDb21pc3NhbyA8fC0tIENvbWlzc2FvTW90b1Jpb0RlSmFuZWlyb1xuSUNvbWlzc2FvIDx8LS0gQ29taXNzYW9Nb25vY2ljbG9SaW9EZUphbmVpcm9cblxuICAiLCJtZXJtYWlkIjp7InRoZW1lIjoiZGFyayJ9LCJ1cGRhdGVFZGl0b3IiOmZhbHNlLCJhdXRvU3luYyI6dHJ1ZSwidXBkYXRlRGlhZ3JhbSI6ZmFsc2V9)](https://mermaid-js.github.io/mermaid-live-editor/edit#eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIElDb21pc3Nhb3tcbiA8PGludGVyZmFjZT4-XG5DYWxjdWxhckNvbWlzc2FvKENhcnJvIGNhcnJvKSBkZWNpbWFsXG59XG5cbmNsYXNzIENvbWlzc2FvQ2Fycm9SaW9EZUphbmVpcm9cblxuY2xhc3MgQ29taXNzYW9Nb3RvUmlvRGVKYW5laXJvXG5cbmNsYXNzIENvbWlzc2FvTW9ub2NpY2xvUmlvRGVKYW5laXJvXG5cbklDb21pc3NhbyA8fC0tIENvbWlzc2FvQ2Fycm9SaW9EZUphbmVpcm9cbklDb21pc3NhbyA8fC0tIENvbWlzc2FvTW90b1Jpb0RlSmFuZWlyb1xuSUNvbWlzc2FvIDx8LS0gQ29taXNzYW9Nb25vY2ljbG9SaW9EZUphbmVpcm9cblxuICAiLCJtZXJtYWlkIjoie1xuICBcInRoZW1lXCI6IFwiZGFya1wiXG59IiwidXBkYXRlRWRpdG9yIjpmYWxzZSwiYXV0b1N5bmMiOnRydWUsInVwZGF0ZURpYWdyYW0iOmZhbHNlfQ)

```mermaid
classDiagram

class IComissao{
 <<interface>>
CalcularComissao(Carro carro) decimal
}

class ComissaoCarroRioDeJaneiro

class ComissaoMotoRioDeJaneiro

class ComissaoMonocicloRioDeJaneiro

IComissao <|-- ComissaoCarroRioDeJaneiro
IComissao <|-- ComissaoMotoRioDeJaneiro
IComissao <|-- ComissaoMonocicloRioDeJaneiro

```

#### Mato Grosso
[![](https://mermaid.ink/img/eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIElDb21pc3Nhb3tcbiA8PGludGVyZmFjZT4-XG5DYWxjdWxhckNvbWlzc2FvKENhcnJvIGNhcnJvKSBkZWNpbWFsXG59XG5cbmNsYXNzIENvbWlzc2FvQ2Fycm9NYXRvR3Jvc3NvXG5cbmNsYXNzIENvbWlzc2FvTW90b01hdG9Hcm9zc29cblxuY2xhc3MgQ29taXNzYW9Nb25vY2ljbG9NYXRvR3Jvc3NvXG5cbklDb21pc3NhbyA8fC0tIENvbWlzc2FvQ2Fycm9NYXRvR3Jvc3NvXG5JQ29taXNzYW8gPHwtLSBDb21pc3Nhb01vdG9NYXRvR3Jvc3NvXG5JQ29taXNzYW8gPHwtLSBDb21pc3Nhb01vbm9jaWNsb01hdG9Hcm9zc29cblxuICAiLCJtZXJtYWlkIjp7InRoZW1lIjoiZGFyayJ9LCJ1cGRhdGVFZGl0b3IiOmZhbHNlLCJhdXRvU3luYyI6dHJ1ZSwidXBkYXRlRGlhZ3JhbSI6ZmFsc2V9)](https://mermaid-js.github.io/mermaid-live-editor/edit#eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIElDb21pc3Nhb3tcbiA8PGludGVyZmFjZT4-XG5DYWxjdWxhckNvbWlzc2FvKENhcnJvIGNhcnJvKSBkZWNpbWFsXG59XG5cbmNsYXNzIENvbWlzc2FvQ2Fycm9NYXRvR3Jvc3NvXG5cbmNsYXNzIENvbWlzc2FvTW90b01hdG9Hcm9zc29cblxuY2xhc3MgQ29taXNzYW9Nb25vY2ljbG9NYXRvR3Jvc3NvXG5cbklDb21pc3NhbyA8fC0tIENvbWlzc2FvQ2Fycm9NYXRvR3Jvc3NvXG5JQ29taXNzYW8gPHwtLSBDb21pc3Nhb01vdG9NYXRvR3Jvc3NvXG5JQ29taXNzYW8gPHwtLSBDb21pc3Nhb01vbm9jaWNsb01hdG9Hcm9zc29cblxuICAiLCJtZXJtYWlkIjoie1xuICBcInRoZW1lXCI6IFwiZGFya1wiXG59IiwidXBkYXRlRWRpdG9yIjpmYWxzZSwiYXV0b1N5bmMiOnRydWUsInVwZGF0ZURpYWdyYW0iOmZhbHNlfQ)

```mermaid
classDiagram

class IComissao{
 <<interface>>
CalcularComissao(Carro carro) decimal
}

class ComissaoCarroMatoGrosso

class ComissaoMotoMatoGrosso

class ComissaoMonocicloMatoGrosso

IComissao <|-- ComissaoCarroMatoGrosso
IComissao <|-- ComissaoMotoMatoGrosso
IComissao <|-- ComissaoMonocicloMatoGrosso

```
Perceba que cada estado brasileiro tem 3 implementações concretas para cada estratégia (__IImposto__ e __IComissao__) e cada vez que pensarmos em vender algo novo em uma localidade são duas implementações concretas que temos que fazer, a vantagem de usar esse pattern é que se quisermos vender algo somente em São Paulo podemos fazer somente mais 2 implementações sem quebrar as implementações do Rio de Janeiro e Mato Grosso, porém como eu disse é praticamente impraticavel utilizar esse pattern pela quantidade de implementações diferentes que vamos ter quando o sistema crescer.
Vou escrever um exemplo de como ficaria esse código porém vendendo só motos e carros nos estados do Rio de Janeiro e São Paulo.
#### Strategy
```csharp
public interface IImposto{
    decimal CalcularImposto(Carro carro);
}

public interface IComissao{
    decimal CalcularComissao(Carro carro);
}
```
#### Rio de Janeiro Implementação
```csharp
public class ImpostoMotoRioDeJaneiro : IImposto {
    public decimal CalcularImposto(Carro carro) => 0.12 * carro.Valor;
}

public class ComissaoMotoRioDeJaneiro : IComissao {
    public decimal CalcularComissao(Carro carro) => 0.08 * carro.Valor;
}

public class ImpostoCarroRioDeJaneiro : IImposto {
    public decimal CalcularImposto(Moto moto) => 0.15 * moto.Valor;
}

public class ComissaoCarroRioDeJaneiro : IComissao {
    public decimal CalcularComissao(Moto moto) => 0.05 * moto.Valor;
}
```

#### São Paulo Implementação
```csharp
public class ImpostoMotoSaoPaulo : IImposto {
    public decimal CalcularImposto(Carro carro) => 0.07 * carro.Valor;
}

public class ComissaoMotoSaoPaulo : IComissao {
    public decimal CalcularComissao(Carro carro) => 0.12 * carro.Valor;
}

public class ImpostoCarroSaoPaulo : IImposto {
    public decimal CalcularImposto(Moto moto) => 0.19 * moto.Valor;
}

public class ComissaoCarroSaoPaulo : IComissao {
    public decimal CalcularComissao(Moto moto) => 0.09 * moto.Valor;
}
```
#### Vendedor Implementação
```csharp
class Vendedor{
    public string Nome {get; set;}
    public Estados Estado {get; set;}
    public decimal Salario {get; set;}
    
    public decimal Vender(Veiculo veiculo, IImposto imposto, IComissao comissao){
        return Salario - comissao.CalcularComissao(veiculo) + imposto.CalcularImposto(veiculo);
    }
}
```
Repare que a implementação do método __Vender__ na classe __Vendedor__ ficou bem menor, entretanto aumentamos a complexidade na hora de chamar o método, perceba ainda que nada impede de passarmos uma implementação de imposto de São Paulo e um calculo de comissão do Rio De Janeiro, ou ainda um calculo de imposto para carro e um de comissão de moto, são erros que podemos cometer ao chamar o método, por isso a quantidade de implementações diferentes dessa solução é considerada um problema. Vamos dar uma olhada na proxima solução e ver como ela resolve esse problema de forma semelhante.
## Introdução ao Visitor
Vou apresentar primeiramente o diagrama UML do padrão, e relacionar com nosso problema.

[![](https://mermaid.ink/img/eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIFZpc2l0b3J7XG4rVmlzaXQoYTogQ29uY3JldGVzRWxlbWVudEEpIHZvaWRcbitWaXNpdChiOiBDb25jcmV0ZUVsZW1lbnRzQikgdm9pZFxufVxuXG5jbGFzcyBDb25jcmV0ZVZpc2l0b3Ixe1xuK1Zpc2l0KGE6IENvbmNyZXRlc0VsZW1lbnRBKSB2b2lkXG4rVmlzaXQoYjogQ29uY3JldGVFbGVtZW50c0IpIHZvaWRcbn1cblxuY2xhc3MgQ29uY3JldGVWaXNpdG9yMntcbitWaXNpdChhOiBDb25jcmV0ZXNFbGVtZW50QSkgdm9pZFxuK1Zpc2l0KGI6IENvbmNyZXRlRWxlbWVudHNCKSB2b2lkXG59XG5cbmNsYXNzIENsaWVudFxuXG5jbGFzcyBFbGVtZW50e1xuK0FjY2VwdCh2aXNpdG9yOiBWaXNpdG9yKSB2b2lkXG59XG5cbmNsYXNzIENvbmNyZXRlRWxlbWVudEF7XG4rQWNjZXB0KHZpc2l0b3I6IFZpc2l0b3IpIHZvaWRcbn1cblxuY2xhc3MgQ29uY3JldGVFbGVtZW50QntcbitBY2NlcHQodmlzaXRvcjogVmlzaXRvcikgdm9pZFxufVxuXG5WaXNpdG9yIDx8LS0gQ29uY3JldGVWaXNpdG9yMVxuVmlzaXRvciA8fC0tIENvbmNyZXRlVmlzaXRvcjJcbkNsaWVudCAtLT4gVmlzaXRvclxuQ2xpZW50IC0tPiBFbGVtZW50XG5FbGVtZW50IDx8LS0gQ29uY3JldGVFbGVtZW50QVxuRWxlbWVudCA8fC0tIENvbmNyZXRlRWxlbWVudEIiLCJtZXJtYWlkIjp7InRoZW1lIjoiZGFyayJ9LCJ1cGRhdGVFZGl0b3IiOmZhbHNlLCJhdXRvU3luYyI6dHJ1ZSwidXBkYXRlRGlhZ3JhbSI6ZmFsc2V9)](https://mermaid-js.github.io/mermaid-live-editor/edit#eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIFZpc2l0b3J7XG4rVmlzaXQoYTogQ29uY3JldGVzRWxlbWVudEEpIHZvaWRcbitWaXNpdChiOiBDb25jcmV0ZUVsZW1lbnRzQikgdm9pZFxufVxuXG5jbGFzcyBDb25jcmV0ZVZpc2l0b3Ixe1xuK1Zpc2l0KGE6IENvbmNyZXRlc0VsZW1lbnRBKSB2b2lkXG4rVmlzaXQoYjogQ29uY3JldGVFbGVtZW50c0IpIHZvaWRcbn1cblxuY2xhc3MgQ29uY3JldGVWaXNpdG9yMntcbitWaXNpdChhOiBDb25jcmV0ZXNFbGVtZW50QSkgdm9pZFxuK1Zpc2l0KGI6IENvbmNyZXRlRWxlbWVudHNCKSB2b2lkXG59XG5cbmNsYXNzIENsaWVudFxuXG5jbGFzcyBFbGVtZW50e1xuK0FjY2VwdCh2aXNpdG9yOiBWaXNpdG9yKSB2b2lkXG59XG5cbmNsYXNzIENvbmNyZXRlRWxlbWVudEF7XG4rQWNjZXB0KHZpc2l0b3I6IFZpc2l0b3IpIHZvaWRcbn1cblxuY2xhc3MgQ29uY3JldGVFbGVtZW50QntcbitBY2NlcHQodmlzaXRvcjogVmlzaXRvcikgdm9pZFxufVxuXG5WaXNpdG9yIDx8LS0gQ29uY3JldGVWaXNpdG9yMVxuVmlzaXRvciA8fC0tIENvbmNyZXRlVmlzaXRvcjJcbkNsaWVudCAtLT4gVmlzaXRvclxuQ2xpZW50IC0tPiBFbGVtZW50XG5FbGVtZW50IDx8LS0gQ29uY3JldGVFbGVtZW50QVxuRWxlbWVudCA8fC0tIENvbmNyZXRlRWxlbWVudEIiLCJtZXJtYWlkIjoie1xuICBcInRoZW1lXCI6IFwiZGFya1wiXG59IiwidXBkYXRlRWRpdG9yIjpmYWxzZSwiYXV0b1N5bmMiOnRydWUsInVwZGF0ZURpYWdyYW0iOmZhbHNlfQ)

```mermaid
classDiagram

class Visitor{
+Visit(a: ConcretesElementA) void
+Visit(b: ConcreteElementsB) void
}

class ConcreteVisitor1{
+Visit(a: ConcretesElementA) void
+Visit(b: ConcreteElementsB) void
}

class ConcreteVisitor2{
+Visit(a: ConcretesElementA) void
+Visit(b: ConcreteElementsB) void
}

class Client

class Element{
+Accept(visitor: Visitor) void
}

class ConcreteElementA{
+Accept(visitor: Visitor) void
}

class ConcreteElementB{
+Accept(visitor: Visitor) void
}

Visitor <|-- ConcreteVisitor1
Visitor <|-- ConcreteVisitor2
Client --> Visitor
Client --> Element
Element <|-- ConcreteElementA
Element <|-- ConcreteElementB

```
Vamos olhar para classe abstrata __Visitor__ lembra do nosso problema onde cada estado tinha uma implementação diferente de imposto e comissão? Que tal se a gente criar visitor especificos para cara estado? Antes disso vamos entender porque isso é uma boa ideia. O __Visitor__ tem um método chamado __Visit ( )__ que tem duas sobrecargas uma recebendo o elemento A e outra recebendo o elemento B certo? Agora pena e se nossos métodos __Visit ()__ recebessem carros, motos e monociclos ? Pois é olha que belezinha porque agora nosso métodos recebem nossos produtos, mas e como resolvemos o problema dos estados? Simples olha la no diagrama que beleza, cada estado pode ser uma classe concreta de __Visitor__. Vamos ver o diagrama adaptado ao nosso problema.

## Aplicação
Ao invés de criar o __Visitor__ como classe vou gerar uma interface, o resultado final é o mesmo nesse caso.

[![](https://mermaid.ink/img/eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIElWaXNpdG9ySW1wb3N0b3tcbjw8aW50ZXJmYWNlPj5cbitDYWxjdWxhckltcG9zdG8obW90bzogTW90bykgZGVjaW1hbFxuK0NhbGN1bGFySW1wb3N0byhjYXJybzogQ2Fycm8pIGRlY2ltYWxcbitDYWxjdWxhckltcG9zdG8obW9ub2NpY2xvOiBNb25vY2ljbG8pIGRlY2ltYWxcbn1cblxuY2xhc3MgU2FvUGF1bG9JbXBvc3RvXG5jbGFzcyBSaW9EZUphbmVpcm9JbXBvc3RvXG5jbGFzcyBNYXRvR3Jvc3NvSW1wb3N0b1xuXG5JVmlzaXRvckltcG9zdG8gPHwtLSBTYW9QYXVsb0ltcG9zdG9cbklWaXNpdG9ySW1wb3N0byA8fC0tIFJpb0RlSmFuZWlyb0ltcG9zdG9cbklWaXNpdG9ySW1wb3N0byA8fC0tIE1hdG9Hcm9zc29JbXBvc3RvXG5cbiIsIm1lcm1haWQiOnsidGhlbWUiOiJkYXJrIn0sInVwZGF0ZUVkaXRvciI6ZmFsc2UsImF1dG9TeW5jIjp0cnVlLCJ1cGRhdGVEaWFncmFtIjpmYWxzZX0)](https://mermaid-js.github.io/mermaid-live-editor/edit#eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIElWaXNpdG9ySW1wb3N0b3tcbjw8aW50ZXJmYWNlPj5cbitDYWxjdWxhckltcG9zdG8obW90bzogTW90bykgZGVjaW1hbFxuK0NhbGN1bGFySW1wb3N0byhjYXJybzogQ2Fycm8pIGRlY2ltYWxcbitDYWxjdWxhckltcG9zdG8obW9ub2NpY2xvOiBNb25vY2ljbG8pIGRlY2ltYWxcbn1cblxuY2xhc3MgU2FvUGF1bG9JbXBvc3RvXG5jbGFzcyBSaW9EZUphbmVpcm9JbXBvc3RvXG5jbGFzcyBNYXRvR3Jvc3NvSW1wb3N0b1xuXG5JVmlzaXRvckltcG9zdG8gPHwtLSBTYW9QYXVsb0ltcG9zdG9cbklWaXNpdG9ySW1wb3N0byA8fC0tIFJpb0RlSmFuZWlyb0ltcG9zdG9cbklWaXNpdG9ySW1wb3N0byA8fC0tIE1hdG9Hcm9zc29JbXBvc3RvXG5cbiIsIm1lcm1haWQiOiJ7XG4gIFwidGhlbWVcIjogXCJkYXJrXCJcbn0iLCJ1cGRhdGVFZGl0b3IiOmZhbHNlLCJhdXRvU3luYyI6dHJ1ZSwidXBkYXRlRGlhZ3JhbSI6ZmFsc2V9)

```mermaid
classDiagram

class IVisitorImposto{
<<interface>>
+CalcularImposto(moto: Moto) decimal
+CalcularImposto(carro: Carro) decimal
+CalcularImposto(monociclo: Monociclo) decimal
}

class SaoPauloImposto
class RioDeJaneiroImposto
class MatoGrossoImposto

IVisitorImposto <|-- SaoPauloImposto
IVisitorImposto <|-- RioDeJaneiroImposto
IVisitorImposto <|-- MatoGrossoImposto
```

[![](https://mermaid.ink/img/eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIElWaXNpdG9yQ29taXNzYW97XG48PGludGVyZmFjZT4-XG4rQ2FsY3VsYXJDb21pc3Nhbyhtb3RvOiBNb3RvKSBkZWNpbWFsXG4rQ2FsY3VsYXJDb21pc3NhbyhjYXJybzogQ2Fycm8pIGRlY2ltYWxcbitDYWxjdWxhckNvbWlzc2FvKG1vbm9jaWNsbzogTW9ub2NpY2xvKSBkZWNpbWFsXG59XG5cbmNsYXNzIFNhb1BhdWxvQ29taXNzYW9cbmNsYXNzIFJpb0RlSmFuZWlyb0NvbWlzc2FvXG5jbGFzcyBNYXRvR3Jvc3NvQ29taXNzYW9cblxuSVZpc2l0b3JDb21pc3NhbyA8fC0tIFNhb1BhdWxvQ29taXNzYW9cbklWaXNpdG9yQ29taXNzYW8gPHwtLSBSaW9EZUphbmVpcm9Db21pc3Nhb1xuSVZpc2l0b3JDb21pc3NhbyA8fC0tIE1hdG9Hcm9zc29Db21pc3Nhb1xuXG4iLCJtZXJtYWlkIjp7InRoZW1lIjoiZGFyayJ9LCJ1cGRhdGVFZGl0b3IiOmZhbHNlLCJhdXRvU3luYyI6dHJ1ZSwidXBkYXRlRGlhZ3JhbSI6ZmFsc2V9)](https://mermaid-js.github.io/mermaid-live-editor/edit#eyJjb2RlIjoiY2xhc3NEaWFncmFtXG5cbmNsYXNzIElWaXNpdG9yQ29taXNzYW97XG48PGludGVyZmFjZT4-XG4rQ2FsY3VsYXJDb21pc3Nhbyhtb3RvOiBNb3RvKSBkZWNpbWFsXG4rQ2FsY3VsYXJDb21pc3NhbyhjYXJybzogQ2Fycm8pIGRlY2ltYWxcbitDYWxjdWxhckNvbWlzc2FvKG1vbm9jaWNsbzogTW9ub2NpY2xvKSBkZWNpbWFsXG59XG5cbmNsYXNzIFNhb1BhdWxvQ29taXNzYW9cbmNsYXNzIFJpb0RlSmFuZWlyb0NvbWlzc2FvXG5jbGFzcyBNYXRvR3Jvc3NvQ29taXNzYW9cblxuSVZpc2l0b3JDb21pc3NhbyA8fC0tIFNhb1BhdWxvQ29taXNzYW9cbklWaXNpdG9yQ29taXNzYW8gPHwtLSBSaW9EZUphbmVpcm9Db21pc3Nhb1xuSVZpc2l0b3JDb21pc3NhbyA8fC0tIE1hdG9Hcm9zc29Db21pc3Nhb1xuXG4iLCJtZXJtYWlkIjoie1xuICBcInRoZW1lXCI6IFwiZGFya1wiXG59IiwidXBkYXRlRWRpdG9yIjpmYWxzZSwiYXV0b1N5bmMiOnRydWUsInVwZGF0ZURpYWdyYW0iOmZhbHNlfQ)

```mermaid
classDiagram

class IVisitorComissao{
<<interface>>
+CalcularComissao(moto: Moto) decimal
+CalcularComissao(carro: Carro) decimal
+CalcularComissao(monociclo: Monociclo) decimal
}

class SaoPauloComissao
class RioDeJaneiroComissao
class MatoGrossoComissao

IVisitorComissao <|-- SaoPauloComissao
IVisitorComissao <|-- RioDeJaneiroComissao
IVisitorComissao <|-- MatoGrossoComissao
```
Pessoal ja da pra ter ideia que o sistema ficou bem mais tranquilo de manter com essa mudança, olha que belezinha se quisermos adicionar mais um estado basta adicionar mais duas classes e implementar os métodos das interfaces __IVisitorComissao__ e __IVisitorImposto__, houve uma grande diminuição de implementações em relação ao pattern anterior (__Strategy__), mas nem tudo são flores repare que todo estado novo devera implementar os métodos de calculo de comissão e imposto para todos os tipos de veiculos mesmo que ele só venda motos por exemplo, isso deixa nossa solução com um certo ponto fraco. Se você esta esperto com o UML do pattern viu que temos que fazer uma pequena mudança nos nossos __ConcreteElements__, eles precisam de um método que receba nossas implementações de visitor.
Primeiramente vou mostrar a implementação para São Paulo dos nossos dois visitors, repare que nossos métodos __Visit ()__ ligados aos __Visitors__ do padrão serão os métodos __CalculaImposto ()__ e __CalculaComissao()__.
### Classe VisitorImpostoSaoPaulo
```csharp
public class VisitorImpostoSaoPaulo: IVisitorImposto{

  public decimal CalculaImposto(Carro carro){
      return carro.Preco * (decimal) 0.1;
  }

  public decimal CalculaImposto(Monociclo monociclo){
      return monociclo.Preco * (decimal) 0.08;
  }

  public decimal CalculaImposto(Moto moto){
      return moto.Preco * (decimal) 0.04;
  }
}
```
### Classe VisitorComissaoSaoPaulo
```csharp
public class VisitorComissaoSaoPaulo: IVisitorImposto{

  public decimal CalculaComissao(Carro carro){
      return carro.Preco * (decimal) 0.01;
  }

  public decimal CalculaComissao(Monociclo monociclo){
      return monociclo.Preco * (decimal) 0.01;
  }

  public decimal CalculaComissao(Moto moto){
      return moto.Preco * (decimal) 0.01;
  }
}
```
Pronto com nossos visitor concretos implementados podemos alterar nossos elementos concretos para serem visitados, nesse momento se atente que o método __Accept ()__ ligado aos __Elements__ do padrão serão nossos métodos __Comissao ()__ e __Imposto ()__.
### Classe Veiculo
```csharp
public abstract class Veiculo
{
  public decimal Preco { get; set; }

  public Veiculo(decimal   preco){
      Preco = preco;
  }
  public abstract decimal Comissao(IVisitorComissao comissao);
  public abstract decimal Imposto(IVisitorImposto visitor);
}

```
Abaixo temos as implementações para cada tipo especifico de __Veiculo__.
### Classe Carro
```csharp
public class Carro: Veiculo{
  public Carro(decimal preco):base(preco){}
    
  public override decimal Comissao(IVisitorComissao visitor){
      return visitor.CalculaComissao(this);
  }
    
  public override decimal Imposto(IVisitorImposto visitor){
      return visitor.CalculaImposto(this);
  }

}
```
### Classe Moto
```csharp
public class Moto: Veiculo{
  public Moto(decimal preco):base(preco){}
    
  public override decimal Comissao(IVisitorComissao visitor){
      return visitor.CalculaComissao(this);
  }
    
  public override decimal Imposto(IVisitorImposto visitor){
      return visitor.CalculaImposto(this);
  }
}
```
### Classe Monociclo
```csharp
public class Monociclo: Veiculo{
  public Monociclo(decimal preco):base(preco){}
    
  public override decimal Comissao(IVisitorComissao visitor){
      return visitor.CalculaComissao(this);
  }
    
  public override decimal Imposto(IVisitorImposto visitor){
      return visitor.CalculaImposto(this);
  }
}
```
### Classe Vendedor
```csharp
public class Vendedor
{
    public static decimal Vender(
        Veiculo veiculo, 
        IVisitorComissao calculadoraComissao,
        IVisitorImposto calculadoraImposto
  )
  {
      decimal comissao = veiculo.Comissao(calculadoraComissao);

      decimal imposto = veiculo.Imposto(calculadoraImposto);

      return veiculo.Preco + imposto - comissao;
  }
}
```
Agora que nosso código esta pronto veja como é facil utliza-lo.
### Utilizando o código 
```csharp
class Program
{
  public static void Main()
  {

      Func<dynamic, string> getName = variable => variable.GetType().Name;

      Veiculo carro = new Carro((decimal) 100_000);

      Veiculo moto = new Moto((decimal) 30_000);

      Veiculo monociclo = new Monociclo((decimal) 1_000);

      IVisitorImposto imposto = new VisitorImpostoSaoPaulo();

      IVisitorComissao comissao = new VisitorComissaoSaoPaulo();

      Veiculo[] veiculos = {carro, moto, monociclo};

      foreach (dynamic veiculo in veiculos){   
          Console.WriteLine($"Valor do {getName(veiculo)}: {veiculo.Preco}");
          Console.WriteLine($"Valor de Venda {getName(veiculo)}: {Vendedor.Vender(veiculo,     comissao, imposto)}\\n");
      }
  }
}
```
### Output
```powershell
Valor do Carro: 100000
Valor de Venda Carro: 109000,00

Valor do Moto: 30000
Valor de Venda Moto: 30900,00

Valor do Monociclo: 1000
Valor de Venda Monociclo: 1070,00
```
Veja como é facil utlizar outros __visitors__ basta implementar um visitor para o Rio de Janeiro ou para qualquer outro estado e utiliza-lo, se você quiser treinar a implementação crie suas implementações para outros estados, troque a instancia no arquivo Program.cs e veja seu o output esta de acordo com suas implementações.
Espero que você tenha gostado, caso tenha alguma duvida estarei postando um video seguindo esse roteiro e implementando do zero.
Link: …
Até Mais