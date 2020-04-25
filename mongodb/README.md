# MongoDB

## Exercício 1 - Aquecendo com os pets

**1. Adicione outro Peixe e um Hamster com nome Frodo**
```
db.pets.insertMany([{name: "Frodo", species: "Peixe"}, {name: "Frodo", species: "Hamster"}])
```
```
{
	"acknowledged" : true,
	"insertedIds" : [
		ObjectId("5ea47abe4ed587f664e869ff"),
		ObjectId("5ea47abe4ed587f664e86a00")
	]
}
```

**2. Faça uma contagem dos pets na coleção**
```
db.pets.count()
```
```
8
```

**3. Retorne apenas um elemento o método prático possível**
```
db.pets.findOne()
```
```
{
	"_id" : ObjectId("5ea47a854ed587f664e869f9"),
	"name" : "Mike",
	"species" : "Hamster"
}
```

**4. Identifique o ID para o Gato Kilha**
```
db.pets.findOne({name: "Kilha", species: "Gato"})._id
```
```
ObjectId("5ea47a854ed587f664e869fb")
```

**5. Faça uma busca pelo ID e traga o Hamster Mike**
```
db.pets.findOne({_id: ObjectId("5ea47a854ed587f664e869f9")})
```
```
{
	"_id" : ObjectId("5ea47a854ed587f664e869f9"),
	"name" : "Mike",
	"species" : "Hamster"
}
```

**6. Use o find para trazer todos os Hamsters**
```
db.pets.find({species: "Hamster"})
```
```
{
	"_id" : ObjectId("5ea47a854ed587f664e869f9"),
	"name" : "Mike",
	"species" : "Hamster"
}
{
	"_id" : ObjectId("5ea47abe4ed587f664e86a00"),
	"name" : "Frodo",
	"species" : "Hamster"
}
```

**7. Use o find para listar todos os pets com nome Mike**
```
db.pets.find({name: "Mike"})
```
```
{
	"_id" : ObjectId("5ea47a854ed587f664e869f9"),
	"name" : "Mike",
	"species" : "Hamster"
}
{
	"_id" : ObjectId("5ea47a854ed587f664e869fc"),
	"name" : "Mike",
	"species" : "Cachorro"
}
```

**8. Liste apenas o documento que é um Cachorro chamado Mike**
```
db.pets.find({name: "Mike", species: "Cachorro"})
```
```
{
	"_id" : ObjectId("5ea47a854ed587f664e869fc"),
	"name" : "Mike",
	"species" : "Cachorro"
}
```

## Exercício 2 - Mama mia!

**1. Liste/Conte todas as pessoas que tem exatamente 99 anos. Você pode usar um count para indicar a quantidade**
```
db.italians.count({age: 99})
```
```
0
```

**2. Identifique quantas pessoas são elegíveis atendimento prioritário (pessoas com mais de 65 anos)**
```
db.italians.count({age: {$gt: 65}})
```
```
1788
```

**3. Identifique todos os jovens (pessoas entre 12 a 18 anos)**
```
db.italians.find({age: {$gte: 12, $lte: 18}}).count()
```
```
900
```

**4. Identifique quantas pessoas tem gatos, quantas tem cachorro e quantas não tem nenhum dos dois**
```
db.italians.find({cat: {$exists: true}}).count()
db.italians.find({dog: {$exists: true}}).count()
db.italians.find({cat: {$exists: false}, dog: {$exists: false}}).count()
```
```
5966
3982
2450
```

**5. Liste/Conte todas as pessoas acima de 60 anos que tenham gato**
```
db.italians.find({age: {$gt: 60}, cat: {$exists: true}}).count()
```
```
1438
```

**6. Liste/Conte todos os jovens com cachorro**
```
db.italians.find({age: {$gte: 12, $lte: 18}, dog: {$exists: true}}).count()
```
```
370
```

**7. Utilizando o $where, liste todas as pessoas que tem gato e cachorro**
```
db.italians.find({$where: "this.cat && this.dog"}).count()
```
```
2398
```

**8. Liste todas as pessoas mais novas que seus respectivos gatos**
```
db.italians.find({$where: "this.cat && this.cat.age > this.age"}).count()
```
```
607
```

**9. Liste as pessoas que tem o mesmo nome que seu bichano (gatou ou cachorro)**
```
db.italians.find({$where: "this.cat && this.cat.name == this.firstname || this.dog && this.dog.name == this.firstname"}).count()
```
```
121
```

**10. Projete apenas o nome e sobrenome das pessoas com tipo de sangue de fator RH negativo**
```
db.italians.find({bloodType: /-/}, {firstname: 1, surname: 1}).count()
```
```
{
	"_id" : ObjectId("5ea485c36ddf987308d361fe"),
	"firstname" : "Vincenzo",
	"surname" : "Benedetti"
}
{
	"_id" : ObjectId("5ea485c36ddf987308d36200"),
	"firstname" : "Cristina",
	"surname" : "Ruggiero"
}
...
```

**11. Projete apenas os animais dos italianos. Devem ser listados os animais com nome e idade. Não mostre o identificado do mongo (ObjectId)**
```
db.italians.find({$or: [{dog: {$exists: true}}, {cat: {$exists: true}}]}, {"dog.name": 1, "dog.age": 1, "cat.name": 1, "cat.age": 1, _id: 0})
```
```
{
	"cat" :	{
		"name" : "Filipo",
		"age" : 0 
	}
}
{
	"dog" :	{
		"name" : "Giacomo",
		"age" : 16
	}
}
...
```

**12. Quais são as 5 pessoas mais velhas com sobrenome Rossi?**
```
db.italians.find({surname: "Rossi"}).sort({age: -1}).limit(5)
```
```
{
	"_id" : ObjectId("5ea485ca6ddf987308d375b0"),
	"firstname" : "Enzo ",
	"surname" : "Rossi",
	"age" : 79
}
{
	"_id" : ObjectId("5ea485c66ddf987308d36aad"),
	"firstname" : "Giacomo",
	"surname" : "Rossi",
	"age" : 78
}
{
	"_id" : ObjectId("5ea485c86ddf987308d36eec"),
	"firstname" : "Serena",
	"surname" : "Rossi",
	"age" : 78
}
{
	"_id" : ObjectId("5ea485cc6ddf987308d37bb2"),
	"firstname" : "Nicola",
	"surname" : "Rossi",
	"age" : 78
}
{
	"_id" : ObjectId("5ea485c46ddf987308d363f3"),
	"firstname" : "Sonia",
	"surname" : "Rossi",
	"age" : 77
}
```

**13. Crie um italiano que tenha um leão como animal de estimação. Associe um nome e idade ao bichano**
```
db.italians.insert({firstname: "italiano 1", surname: "teste 1", lion: {name: "Leao 1", age: 7}})
```
```
ObjectId("5ea4986443a18f256e0c3eb1")
```

**14. Infelizmente o Leão comeu o italiano. Remova essa pessoa usando o Id**
```
db.italians.findOne({firstname: "italiano 1"})._id
```
```
db.italians.remove({_id: ObjectId("5ea4986443a18f256e0c3eb1")})
```

**15. Passou um ano. Atualize a idade de todos os italianos e dos bichanos em 1**
```
db.italians.updateMany({}, {$inc: {age: 1}})
db.italians.updateMany({dog: {$exists: true}}, {$inc: {"dog.age": 1}})
db.italians.updateMany({cat: {$exists: true}}, {$inc: {"cat.age": 1}})
db.italians.updateMany({mother: {$exists: true}}, {$inc: {"mother.age": 1}})
db.italians.updateMany({father: {$exists: true}}, {$inc: {"father.age": 1}})
```
```
{
	"acknowledged" : true,
	"matchedCount" : 10000,
	"modifiedCount" : 10000
}
{
	"acknowledged" : true,
	"matchedCount" : 3982,
	"modifiedCount" : 3982
}
{
	"acknowledged" : true,
	"matchedCount" : 5966,
	"modifiedCount" : 5966
}
{
	"acknowledged" : true,
	"matchedCount" : 1003,
	"modifiedCount" : 1003
}
{
	"acknowledged" : true,
	"matchedCount" : 2018,
	"modifiedCount" : 2018
}
```

**16. O Corona Vírus chegou na Itália e misteriosamente atingiu pessoas somente com gatos e de 66 anos. Remova esses italianos**
```
db.italians.deleteMany({cat: {$exists : true}, age: 66})
```
```
{
	"acknowledged": true,
	"deletedCount": 64
}
```

**17. Utilizando o framework agregate, liste apenas as pessoas com nomes iguais a sua respectiva mãe e que tenha gato ou cachorro**
```
```
```
```

**18. Utilizando aggregate framework, faça uma lista de nomes única de nomes. Faça isso usando apenas o primeiro nome**
```
```
```
```

**19. Agora faça a mesma lista do item acima, considerando nome completo**
```
```
```
```

**20. Procure pessoas que gosta de Banana ou Maçã, tenham cachorro ou gato, mais de 20 e  menos de 60 anos**
```
```
```
```
