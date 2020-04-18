## Exercício 1 - Aquecendo com os pets

### 1. Adicione outro Peixe e um Hamster com nome Frodo
> db.pets.insert({name: "Frodo", species: "Peixe"})
<br> **WriteResult({ "nInserted" : 1 })**

> db.pets.insert({name: "Frodo", species: "Hamster"})
<br> **WriteResult({ "nInserted" : 1 })**

### 2. Faça uma contagem dos pets na coleção
> db.pets.count()
<br> **8**

### 3. Retorne apenas um elemento o método prático possível
> db.pets.findOne()
<br> **{ "_id" : ObjectId("5e9afc92fb668ced72cd6798"), "name" : "Mike", "species" : "Hamster" }**

### 4. Identifique o ID para o Gato Kilha
> db.pets.findOne({name: "Kilha", species: "Gato"})._id
<br> **ObjectId("5e9afcacfb668ced72cd679a")**

### 5. Faça uma busca pelo ID e traga o Hamster Mike
> db.pets.findOne({name: "Mike", species: "Hamster"})
<br> **{ "_id" : ObjectId("5e9afc92fb668ced72cd6798"), "name" : "Mike", "species" : "Hamster" }**

> db.pets.findOne({_id: ObjectId("5e9afc92fb668ced72cd6798")})
<br> **{ "_id" : ObjectId("5e9afc92fb668ced72cd6798"), "name" : "Mike", "species" : "Hamster" }**

### 6. Use o find para trazer todos os Hamsters
> db.pets.find({species: "Hamster"})
<br> **{ "_id" : ObjectId("5e9afc92fb668ced72cd6798"), "name" : "Mike", "species" : "Hamster" }**
<br> **{ "_id" : ObjectId("5e9afe7bfb668ced72cd679f"), "name" : "Frodo", "species" : "Hamster" }**

### 7. Use o find para listar todos os pets com nome Mike
> db.pets.find({name: "Mike"})
<br> **{ "_id" : ObjectId("5e9afc92fb668ced72cd6798"), "name" : "Mike", "species" : "Hamster" }**
<br> **{ "_id" : ObjectId("5e9afcb4fb668ced72cd679b"), "name" : "Mike", "species" : "Cachorro" }**

### 8. Liste apenas o documento que é um Cachorro chamado Mike
> db.pets.find({name: "Mike", species: "Cachorro"})
<br> **{ "_id" : ObjectId("5e9afcb4fb668ced72cd679b"), "name" : "Mike", "species" : "Cachorro" }**

## Exercício 2 - Mama mia!

### 1. Liste/Conte todas as pessoas que tem exatamente 99 anos. Você pode usar um count para indicar a quantidade
> db.italians.count({age: 99})
<br> **0**

### 2. Identifique quantas pessoas são elegíveis atendimento prioritário (pessoas com mais de 65 anos)
> db.italians.count({age: {$gte: 65}})
<br> **1922**

### 3. Identifique todos os jovens (pessoas entre 12 a 18 anos)
> db.italians.find({age: {$in: [12,18]}})
<br> **{ "_id" : ObjectId("5e9b0628fb668ced72cd8ed4"), "firstname" : "Vincenzo", "surname" : "Testa", "username" : "user136", "age" : 18, "email" : "Vincenzo.Testa@outlook.com", "bloodType" : "AB+", "id_num" : "522743746024", "registerDate" : ISODate("2015-12-08T10:53:12.408Z"), "ticketNumber" : 5613, "jobs" : [ "Design", "Irrigação e Drenagem" ], "favFruits" : [ "Kiwi", "Goiaba", "Goiaba" ], "movies" : [ { "title" : "O Senhor dos Anéis: A Sociedade do Anel (2001)", "rating" : 2.45 }, { "title" : "Os Bons Companheiros (1990)", "rating" : 2.44 }, { "title" : "Star Wars, Episódio V: O Império Contra-Ataca (1980)", "rating" : 0.86 } ], "mother" : { "firstname" : "Eleonora", "surname" : "Testa", "age" : 45 }, "cat" : { "name" : "Simona", "age" : 12 }, "dog" : { "name" : "Manuela", "age" : 5 } }**
<br> **{ "_id" : ObjectId("5e9b0628fb668ced72cd8f15"), "firstname" : "Giovanna", "surname" : "Marino", "username" : "user201", "age" : 12, "email" : "Giovanna.Marino@yahoo.com", "bloodType" : "B+", "id_num" : "866452500043", "registerDate" : ISODate("2012-11-19T18:55:08.900Z"), "ticketNumber" : 1668, "jobs" : [ "Geoprocessamento", "Psicopedagogia" ], "favFruits" : [ "Goiaba", "Pêssego" ], "movies" : [ { "title" : "12 Homens e uma Sentença (1957)", "rating" : 1.75 }, { "title" : "Guerra nas Estrelas (1977)", "rating" : 1.39 }, { "title" : "Pulp Fiction: Tempo de Violência (1994)", "rating" : 1.81 } ], "cat" : { "name" : "Enrico", "age" : 10 } }**
<br> **{ "_id" : ObjectId("5e9b0628fb668ced72cd8f42"), "firstname" : "Alex", "surname" : "Rinaldi", "username" : "user246", "age" : 12, "email" : "Alex.Rinaldi@live.com", "bloodType" : "O-", "id_num" : "105208232178", "registerDate" : ISODate("2012-04-07T09:29:20.508Z"), "ticketNumber" : 6957, "jobs" : [ "Gestão de Turismo", "Marketing" ], "favFruits" : [ "Tangerina" ], "movies" : [ { "title" : "Os Sete Samurais (1954)", "rating" : 4.52 }, { "title" : "A Vida é Bela (1997)", "rating" : 2.29 }, { "title" : "Um Estranho no Ninho (1975)", "rating" : 1.26 }, { "title" : "Harakiri (1962)", "rating" : 2.1 }, { "title" : "Pulp Fiction: Tempo de Violência (1994)", "rating" : 1.62 } ], "cat" : { "name" : "Davide", "age" : 13 } }**
<br> **...**

### 4. Identifique quantas pessoas tem gatos, quantas tem cachorro e quantas não tem nenhum dos dois
>
<br> ****

### 5. Liste/Conte todas as pessoas acima de 60 anos que tenham gato
>
<br> ****

### 6. Liste/Conte todos os jovens com cachorro
>
<br> ****

### 7. Utilizando o $where, liste todas as pessoas que tem gato e cachorro
>
<br> ****

### 8. Liste todas as pessoas mais novas que seus respectivos gatos
>
<br> ****

### 9. Liste as pessoas que tem o mesmo nome que seu bichano (gatou ou cachorro)
>
<br> ****

### 10. Projete apenas o nome e sobrenome das pessoas com tipo de sangue de fator RH negativo
>
<br> ****

### 11. Projete apenas os animais dos italianos. Devem ser listados os animais com nome e idade. Não mostre o identificado do mongo (ObjectId)
>
<br> ****

### 12. Quais são as 5 pessoas mais velhas com sobrenome Rossi?
>
<br> ****

### 13. Crie um italiano que tenha um leão como animal de estimação. Associe um nome e idade ao bichano
>
<br> ****

### 14. Infelizmente o Leão comeu o italiano. Remova essa pessoa usando o Id
>
<br> ****

### 15. Passou um ano. Atualize a idade de todos os italianos e dos bichanos em 1
>
<br> ****

### 16. O Corona Vírus chegou na Itália e misteriosamente atingiu pessoas somente com gatos e de 66 anos. Remova esses italianos
>
<br> ****

### 17. Utilizando o framework agregate, liste apenas as pessoas com nomes iguais a sua respectiva mãe e que tenha gato ou cachorro
>
<br> ****

### 18. Utilizando aggregate framework, faça uma lista de nomes única de nomes. Faça isso usando apenas o primeiro nome
>
<br> ****

### 19. Agora faça a mesma lista do item acima, considerando nome completo
>
<br> ****

### 20. Procure pessoas que gosta de Banana ou Maçã, tenham cachorro ou gato, mais de 20 e  menos de 60 anos
>
<br> ****
