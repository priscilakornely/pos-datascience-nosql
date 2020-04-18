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