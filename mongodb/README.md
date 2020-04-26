# MongoDB

## Exercício 1 - Aquecendo com os pets

**1. Adicione outro Peixe e um Hamster com nome Frodo**
```
db.pets.insertMany([{name: "Frodo", species: "Peixe"}, {name: "Frodo", species: "Hamster"}])
```
```
{ "acknowledged" : true, "insertedIds" : [ ObjectId("5ea47abe4ed587f664e869ff"), ObjectId("5ea47abe4ed587f664e86a00") ] }
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
{ "_id" : ObjectId("5ea47a854ed587f664e869f9"), "name" : "Mike", "species" : "Hamster" }
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
{ "_id" : ObjectId("5ea47a854ed587f664e869f9"), "name" : "Mike", "species" : "Hamster" }
```

**6. Use o find para trazer todos os Hamsters**
```
db.pets.find({species: "Hamster"})
```
```
{ "_id" : ObjectId("5ea47a854ed587f664e869f9"), "name" : "Mike", "species" : "Hamster" }
{ "_id" : ObjectId("5ea47abe4ed587f664e86a00"),	"name" : "Frodo", "species" : "Hamster" }
```

**7. Use o find para listar todos os pets com nome Mike**
```
db.pets.find({name: "Mike"})
```
```
{ "_id" : ObjectId("5ea47a854ed587f664e869f9"),	"name" : "Mike", "species" : "Hamster" }
{ "_id" : ObjectId("5ea47a854ed587f664e869fc"),	"name" : "Mike", "species" : "Cachorro" }
```

**8. Liste apenas o documento que é um Cachorro chamado Mike**
```
db.pets.find({name: "Mike", species: "Cachorro"})
```
```
{ "_id" : ObjectId("5ea47a854ed587f664e869fc"), "name" : "Mike", "species" : "Cachorro" }
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
{ "_id" : ObjectId("5ea485c36ddf987308d361fe"), "firstname" : "Vincenzo", "surname" : "Benedetti" }
{ "_id" : ObjectId("5ea485c36ddf987308d36200"), "firstname" : "Cristina", "surname" : "Ruggiero" }
...
```

**11. Projete apenas os animais dos italianos. Devem ser listados os animais com nome e idade. Não mostre o identificado do mongo (ObjectId)**
```
db.italians.find({$or: [{dog: {$exists: true}}, {cat: {$exists: true}}]}, {"dog.name": 1, "dog.age": 1, "cat.name": 1, "cat.age": 1, _id: 0})
```
```
{ "cat" : { "name" : "Filipo", "age" : 0 } }
{ "dog" : { "name" : "Giacomo", "age" : 16 } }
...
```

**12. Quais são as 5 pessoas mais velhas com sobrenome Rossi?**
```
db.italians.find({surname: "Rossi"}).sort({age: -1}).limit(5)
```
```
{ "_id" : ObjectId("5ea485ca6ddf987308d375b0"), "firstname" : "Enzo ", "surname" : "Rossi", "age" : 79 }
{ "_id" : ObjectId("5ea485c66ddf987308d36aad"), "firstname" : "Giacomo", "surname" : "Rossi", "age" : 78 }
{ "_id" : ObjectId("5ea485c86ddf987308d36eec"), "firstname" : "Serena", "surname" : "Rossi", "age" : 78 }
{ "_id" : ObjectId("5ea485cc6ddf987308d37bb2"), "firstname" : "Nicola", "surname" : "Rossi", "age" : 78 }
{ "_id" : ObjectId("5ea485c46ddf987308d363f3"), "firstname" : "Sonia", "surname" : "Rossi", "age" : 77 }
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
{ "acknowledged" : true, "matchedCount" : 10000, "modifiedCount" : 10000 }
{ "acknowledged" : true, "matchedCount" : 3982, "modifiedCount" : 3982 }
{ "acknowledged" : true, "matchedCount" : 5966, "modifiedCount" : 5966 }
{ "acknowledged" : true, "matchedCount" : 1003, "modifiedCount" : 1003 }
{ "acknowledged" : true, "matchedCount" : 2018, "modifiedCount" : 2018 }
```

**16. O Corona Vírus chegou na Itália e misteriosamente atingiu pessoas somente com gatos e de 66 anos. Remova esses italianos**
```
db.italians.deleteMany({cat: {$exists: true}, age: 66})
```
```
{ "acknowledged": true, "deletedCount": 64 }
```

**17. Utilizando o framework agregate, liste apenas as pessoas com nomes iguais a sua respectiva mãe e que tenha gato ou cachorro**
```
db.italians.aggregate([ {$match: {$and: [{mother: {$exists: true}}, {$or: [{dog: {$exists: true}}, {cat: {$exists: true}}]}]}}, {$project: {_id: 0, firstname: 1, mother: 1, cat: 1, dog: 1, isEqual: {$cmp: ["$firstname", "$mother.firstname"]}}}, {$match: {isEqual: 0}} ])

```
```
{ "firstname" : "Elena", "mother" : { "firstname" : "Elena", "surname" : "Caputo", "age" : 86 }, "cat" : { "name" : "Sara", "age" : 7 }, "isEqual" : 0 }
{ "firstname" : "Marco", "mother" : { "firstname" : "Marco", "surname" : "Caruso", "age" : 79 }, "dog" : { "name" : "Giusy", "age" : 10 }, "isEqual" : 0 }
{ "firstname" : "Daniele", "mother" : { "firstname" : "Daniele", "surname" : "Grassi", "age" : 22 }, "cat" : { "name" : "Mirko", "age" : 1 }, "isEqual" : 0 }
{ "firstname" : "Nicola", "mother" : { "firstname" : "Nicola", "surname" : "Bianco", "age" : 46 }, "cat" : { "name" : "Manuela", "age" : 2 }, "isEqual" : 0 }
{ "firstname" : "Daniele", "mother" : { "firstname" : "Daniele", "surname" : "Ferri", "age" : 45 }, "cat" : { "name" : "Raffaele", "age" : 11 }, "isEqual" : 0 }
{ "firstname" : "Marta", "mother" : { "firstname" : "Marta", "surname" : "Benedetti", "age" : 59 }, "cat" : { "name" : "Paolo", "age" : 2 }, "isEqual" : 0 }
{ "firstname" : "Sonia", "mother" : { "firstname" : "Sonia", "surname" : "Caputo", "age" : 69 }, "cat" : { "name" : "Laura", "age" : 13 }, "dog" : { "name" : "Davide", "age" : 16 }, "isEqual" : 0 }
{ "firstname" : "Martina", "mother" : { "firstname" : "Martina", "surname" : "Battaglia", "age" : 82 }, "cat" : { "name" : "Nicola", "age" : 4 }, "isEqual" : 0 }
{ "firstname" : "Giorgio", "mother" : { "firstname" : "Giorgio", "surname" : "Bruno", "age" : 93 }, "cat" : { "name" : "Andrea", "age" : 17 }, "isEqual" : 0 }
{ "firstname" : "Giorgia", "mother" : { "firstname" : "Giorgia", "surname" : "Moretti", "age" : 38 }, "cat" : { "name" : "Sergio", "age" : 11 }, "dog" : { "name" : "Salvatore", "age" : 7 }, "isEqual" : 0 }
{ "firstname" : "Tiziana", "mother" : { "firstname" : "Tiziana", "surname" : "Marini", "age" : 81 }, "cat" : { "name" : "Valeira", "age" : 5 }, "dog" : { "name" : "Roberto", "age" : 11 }, "isEqual" : 0 }
```

**18. Utilizando aggregate framework, faça uma lista de nomes única de nomes. Faça isso usando apenas o primeiro nome**
```
db.italians.aggregate([{$group: {_id: {firstname: "$firstname"}}}])
```
```
{ "_id" : { "firstname" : "Silvia" } }
{ "_id" : { "firstname" : "Elisa" } }
{ "_id" : { "firstname" : "Simona" } }
{ "_id" : { "firstname" : "Cristina" } }
{ "_id" : { "firstname" : "Giovanni" } }
{ "_id" : { "firstname" : "Andrea" } }
{ "_id" : { "firstname" : "Salvatore" } }
{ "_id" : { "firstname" : "Stefania" } }
{ "_id" : { "firstname" : "Angela" } }
...
```

**19. Agora faça a mesma lista do item acima, considerando nome completo**
```
db.italians.aggregate([{$group: {_id: {firstname: "$firstname", surname: "$surname"}}}])
```
```
{ "_id" : { "firstname" : "Enzo ", "surname" : "Bianchi" } }
{ "_id" : { "firstname" : "Emanuele", "surname" : "Palumbo" } }
{ "_id" : { "firstname" : "Sara", "surname" : "Conti" } }
{ "_id" : { "firstname" : "Chiara", "surname" : "Giordano" } }
{ "_id" : { "firstname" : "Daniele", "surname" : "Ferri" } }
{ "_id" : { "firstname" : "Ilaria", "surname" : "Marchetti" } }
{ "_id" : { "firstname" : "Gabiele", "surname" : "Mazza" } }
{ "_id" : { "firstname" : "Pasquale", "surname" : "Grasso" } }
...
```

**20. Procure pessoas que gosta de Banana ou Maçã, tenham cachorro ou gato, mais de 20 e  menos de 60 anos**
```
db.italians.aggregate([ {$match: {$and: [{favFruits: {$in: ["Banana", "Maçã"]}}, {age: {$gt: 20, $lt: 60}}, {$or: [{dog: {$exists: true}}, {cat: {$exists: true}}]}]}}, {$project: {_id: 0, firstname: 1, favFruits: 1, age: 1, cat: 1, dog: 1}} ])
```
```
{ "firstname" : "Federica", "age" : 25, "favFruits" : [ "Banana", "Laranja" ], "cat" : { "name" : "Eleonora", "age" : 4 } }
{ "firstname" : "Pietro", "age" : 59, "favFruits" : [ "Goiaba", "Banana" ], "cat" : { "name" : "Vincenzo", "age" : 14 } }
{ "firstname" : "Lucia", "age" : 25, "favFruits" : [ "Uva", "Banana" ], "cat" : { "name" : "Fabio", "age" : 13 }, "dog" : { "name" : "Tiziana", "age" : 5 } }
{ "firstname" : "Marta", "age" : 28, "favFruits" : [ "Mamão", "Maçã" ], "cat" : { "name" : "Monica", "age" : 4 } }
{ "firstname" : "Sonia", "age" : 30, "favFruits" : [ "Banana" ], "dog" : { "name" : "Mauro", "age" : 4 } }
{ "firstname" : "Massimo", "age" : 35, "favFruits" : [ "Kiwi", "Banana" ], "cat" : { "name" : "Giorgio", "age" : 13 } }
{ "firstname" : "Laura", "age" : 41, "favFruits" : [ "Maçã", "Tangerina", "Laranja" ], "cat" : { "name" : "Fabrizio", "age" : 15 } }
{ "firstname" : "Gianni", "age" : 53, "favFruits" : [ "Laranja", "Goiaba", "Maçã" ], "cat" : { "name" : "Manuela", "age" : 9 } }
{ "firstname" : "Cinzia", "age" : 48, "favFruits" : [ "Uva", "Maçã" ], "cat" : { "name" : "Mauro", "age" : 14 }, "dog" : { "name" : "Monica", "age" : 12 } }
{ "firstname" : "Giorgia", "age" : 27, "favFruits" : [ "Banana", "Goiaba" ], "dog" : { "name" : "Giovanni", "age" : 6 } }
{ "firstname" : "Claudia", "age" : 46, "favFruits" : [ "Laranja", "Melancia", "Maçã" ], "dog" : { "name" : "Laura", "age" : 10 } }
{ "firstname" : "Alex", "age" : 47, "favFruits" : [ "Maçã" ], "cat" : { "name" : "Maria", "age" : 14 } }
{ "firstname" : "Nicola", "age" : 54, "favFruits" : [ "Banana", "Mamão", "Maçã" ], "cat" : { "name" : "Giacomo", "age" : 2 } }
{ "firstname" : "Sonia", "age" : 52, "favFruits" : [ "Mamão", "Banana", "Pêssego" ], "cat" : { "name" : "Manuela", "age" : 10 } }
{ "firstname" : "Laura", "age" : 22, "favFruits" : [ "Tangerina", "Banana", "Kiwi" ], "cat" : { "name" : "Rosa", "age" : 8 } }
{ "firstname" : "Daniela", "age" : 26, "favFruits" : [ "Maçã" ], "dog" : { "name" : "Giorgia", "age" : 7 } }
{ "firstname" : "Barbara", "age" : 37, "favFruits" : [ "Kiwi", "Banana", "Melancia" ], "cat" : { "name" : "Daniela", "age" : 8 }, "dog" : { "name" : "Sonia", "age" : 13 } }
{ "firstname" : "Pasquale", "age" : 26, "favFruits" : [ "Banana" ], "cat" : { "name" : "Michela", "age" : 1 }, "dog" : { "name" : "Gianluca", "age" : 17 } }
{ "firstname" : "Sonia", "age" : 41, "favFruits" : [ "Banana" ], "dog" : { "name" : "Riccardo", "age" : 2 } }
{ "firstname" : "Federica", "age" : 31, "favFruits" : [ "Tangerina", "Maçã" ], "cat" : { "name" : "Mauro", "age" : 9 }, "dog" : { "name" : "Mattia", "age" : 4 } }
...
```

## Exercício 3 - Stockbrokers

**1. Liste as ações com profit acima de 0.5 (limite a 10 o resultado)**
```
db.stocks.find({"Profit Margin": {$gt: 0.5}}, {_id: 0, Ticker: 1, Sector: 1, "Profit Margin": 1}).limit(10)
```
```
{ "Ticker" : "AB", "Profit Margin" : 0.896, "Sector" : "Financial" }
{ "Ticker" : "AGNC", "Profit Margin" : 0.972, "Sector" : "Financial" }
{ "Ticker" : "ARCC", "Profit Margin" : 0.654, "Sector" : "Financial" }
{ "Ticker" : "ARI", "Profit Margin" : 0.576, "Sector" : "Financial" }
{ "Ticker" : "ARR", "Profit Margin" : 0.848, "Sector" : "Financial" }
{ "Ticker" : "ATHL", "Profit Margin" : 0.732, "Sector" : "Basic Materials" }
{ "Ticker" : "AYR", "Profit Margin" : 0.548, "Sector" : "Services" }
{ "Ticker" : "BK", "Profit Margin" : 0.63, "Sector" : "Financial" }
{ "Ticker" : "BLX", "Profit Margin" : 0.588, "Sector" : "Financial" }
{ "Ticker" : "BPO", "Profit Margin" : 0.503, "Sector" : "Financial" }
```

**2. Liste as ações com perdas (limite a 10 novamente)**
```
db.stocks.find({"Profit Margin": {$lt: 0}}, {_id: 0, Ticker: 1, Sector: 1, "Profit Margin": 1}).limit(10)
```
```
{ "Ticker" : "AAOI", "Profit Margin" : -0.023, "Sector" : "Technology" }
{ "Ticker" : "AAV", "Profit Margin" : -0.232, "Sector" : "Basic Materials" }
{ "Ticker" : "ABCD", "Profit Margin" : -0.645, "Sector" : "Services" }
{ "Ticker" : "ABFS", "Profit Margin" : -0.005, "Sector" : "Services" }
{ "Ticker" : "ABMC", "Profit Margin" : -0.0966, "Sector" : "Healthcare" }
{ "Ticker" : "ABX", "Profit Margin" : -0.769, "Sector" : "Basic Materials" }
{ "Ticker" : "ACCL", "Profit Margin" : -0.014, "Sector" : "Technology" }
{ "Ticker" : "ACFC", "Profit Margin" : -0.18, "Sector" : "Financial" }
{ "Ticker" : "ACH", "Profit Margin" : -0.051, "Sector" : "Basic Materials" }
{ "Ticker" : "ACI", "Profit Margin" : -0.173, "Sector" : "Basic Materials" }
```

**3. Liste as 10 ações mais rentáveis**
```
db.stocks.find({}, {_id: 0, Ticker: 1, Sector: 1, "Profit Margin": 1}).sort({"Profit Margin": -1}).limit(10)
```
```
{ "Ticker" : "BPT", "Profit Margin" : 0.994, "Sector" : "Basic Materials" }
{ "Ticker" : "CACB", "Profit Margin" : 0.994, "Sector" : "Financial" }
{ "Ticker" : "ROYT", "Profit Margin" : 0.99, "Sector" : "Basic Materials" }
{ "Ticker" : "NDRO", "Profit Margin" : 0.986, "Sector" : "Basic Materials" }
{ "Ticker" : "WHZ", "Profit Margin" : 0.982, "Sector" : "Basic Materials" }
{ "Ticker" : "MVO", "Profit Margin" : 0.976, "Sector" : "Basic Materials" }
{ "Ticker" : "AGNC", "Profit Margin" : 0.972, "Sector" : "Financial" }
{ "Ticker" : "VOC", "Profit Margin" : 0.971, "Sector" : "Basic Materials" }
{ "Ticker" : "MTR", "Profit Margin" : 0.97, "Sector" : "Financial" }
{ "Ticker" : "OLP", "Profit Margin" : 0.97, "Sector" : "Financial" }
```

**4. Qual foi o setor mais rentável?**
```
db.stocks.aggregate([{$group: {_id: "$Sector", total: {$sum: "$Profit Margin"}}}, {$sort: {total: -1}}, {$limit: 1}])
```
```
{ "_id" : "Financial", "total" : 162.5356 }
```

**5. Ordene as ações pelo profit e usando um cursor, liste as ações**
```
var cursor = db.stocks.find({}, {_id: 0, Ticker: 1, Sector: 1, "Profit Margin": 1}).sort({"$Profit Margin": 1})
cursor.next()
```
```
{ "Ticker" : "A", "Profit Margin" : 0.137, "Sector" : "Healthcare" }
```

**6. Renomeie o campo “Profit Margin” para apenas “profit”**
```
db.stocks.updateMany({}, {$rename : {"Profit Margin" : "profit"}})
```
```
{ "acknowledged" : true, "matchedCount" : 6756, "modifiedCount" : 4302 }
```

**7. Agora liste apenas a empresa e seu respectivo resultado**
```
db.stocks.find({profit: {$exists: true}}, {_id: 0, Company: 1, profit: 1})
```
```
{ "Company" : "Agilent Technologies Inc.", "profit" : 0.137 }
{ "Company" : "Alcoa, Inc.", "profit" : 0.013 }
{ "Company" : "Atlantic American Corp.", "profit" : 0.056 }
{ "Company" : "Aaron's, Inc.", "profit" : 0.06 }
{ "Company" : "Applied Optoelectronics, Inc.", "profit" : -0.023 }
{ "Company" : "AAON Inc.", "profit" : 0.105 }
...
```

**8. Analise as ações. É uma bola de cristal na sua mão... Quais as três ações você investiria?**
```
db.stocks.find({}, {_id: 0, "Performance (Month)": 1, Company:1}).sort({"Performance (Month)": -1}).limit(3)
```
```
{ "Company" : "Oxygen Biotherapeutics, Inc.", "Performance (Month)" : 3.7761 }
{ "Company" : "ARC Wireless Solutions Inc.", "Performance (Month)" : 3.4211 }
{ "Company" : "Champion Industries Inc.", "Performance (Month)" : 1.3913 }
```

**9. Liste as ações agrupadas por setor**
```
db.stocks.aggregate([{$group: {_id: "$Sector", stocks: {$addToSet: "$Ticker"}}}])
```
```
{ "_id" : "Basic Materials", "stocks" : [ "LODE", "NFG", "QEP", "TEP", "MPLX", "KGJI", "CVRR", "ANW", "EQU", "GRA", "KOG", "CPE", "NOA", "SHI", "GSM", "ZAZA", "SLB", "RGLD", "URRE", "EROC", "HK", "FOE", "PNRG", "ROCK", "ROYT", "OCIP", "BRN", "CVE", "DDC", "JRCC", "GNE", "PLM", "REI", "TAHO", "ALB", "TPLM", "GPOR", "IOC", "NOR", "HEP", "END", "NG", "PXD", "DO", "PZG", "BTU", "VTG", "GURE", "LYB", "PKD", "SD", "FXEN", "GTU", "MPC", "AEM", "RNO", "ARG", "ACH", "DVN", "CVI", "GDP", "GORO", "SE", "HDY", "PEIX", "PLG", "ACI", "CBT", "GGS", "EPB", "FSM", "LEI", "SWC", "SCEI", "CDE", "THM", "TGB", "CEO", "VLO", "CDY", "CLD", "FISH", "BHI", "CRZO", "PVA", "MBLX", "ENB", "MDW", "BRY", "MNGA", "IPHS", "PPP", "PDO", "ATHL", "AVD", "DOW", "CIE", "AU", "MEOH", "OSN", "MCF", "PBT", "GGB", "IOSP", "MPET", "KIOR", "CHGS", "SID", "EMN", "WPZ", "SWN", "CXO", "EOX", "REX", "NBL", "NGL", "PKX", "PSE", "WHX", "TESO", "HLX", "RECV", "AUMN", "SCCO", "XCO", "PES", "CHMT", "DKL", "OCIR", "VGZ", "CYT", "NUE", "IVAN", "GG", "RCON", "ESV", "OII", "PWE", "CERE", "LGCY", "UEC", "MTDR", "NFX", "RDC", "FET", "DD", "EXLP", "BRD", "SDLP", "ALDW", "TAM", "HWKN", "HOS", "RIG", "UAMY", "APC", "TGA", "FRD", "NSH", "FST", "SYRG", "CMC", "TX", "NAK", "ASM", "ECA", "MBII", "MON", "MVG", "SLCA", "PAAS", "ODC", "PBM", "TGC", "KRO", "TROX", "BAA", "RRMS", "CPSL", "DWSN", "ARLP", "LNG", "LPI", "KMP", "GEL", "ACMP", "BP", "GTE", "TLM", "CENX", "BOLT", "AR", "MTL", "EVEP", "ATL", "IFNY", "LTBR", "AVL", "SSL", "APFC", "PSTR", "CQP", "NGD", "SDRL", "SOQ", "SXL", "WRES", "DNN", "CJES", "PZE", "APL", "SXC", "PQ", "CRT", "MMLP", "KBX", "RPM", "FCX", "MMP", "SVLC", "NR", "RTK", "OXF", "MIL", "WFT", "NE", "VNR", "LRE", "WLB", "NWPX", "IMO", "LNCO", "CRR", "FSI", "MEMP", "ECT", "MDM", "OAS", "ROSE", "AGI", "NSLP", "SMG", "SNMX", "MILL", "HP", "HNR", "WH", "WLT", "VHI", "PVG", "ANV", "KRA", "SRLP", "MEIL", "KWR", "SJT", "NGS", "BIOA", "AKS", "PDCE", "STLD", "IKNX", "MACE", "POT", "CLR", "DPM", "MXC", "RNF", "ABX", "PED", "SIAL", "ACO", "BXE", "MGH", "SGU", "ZINC", "PAL", "MPO", "AMRS", "CHOP", "CLB", "ETP", "AAU", "EQM", "PBF", "MGN", "SYMX", "AKG", "RDS-B", "XPL", "RBY", "BCEI", "CMP", "NS", "IFF", "REXX", "X", "UNT", "ETE", "BAK", "ESTE", "QEPM", "BIOF", "NTI", "PBR", "RVM", "FI", "BPL", "GFI", "SLW", "VOC", "MWE", "CEP", "HCLP", "KEG", "COG", "LIWA", "MRO", "NOV", "QMM", "GNI", "ARP", "SA", "GSJK", "MTX", "TCP", "XTXI", "WLL", "RGP", "OLN", "TAT", "TRQ", "RIO", "CMLP", "EPM", "HUN", "AE", "SBGL", "AXAS", "BVN", "NBR", "SNP", "USEG", "DBLE", "BPT", "RIC", "ANR", "COP", "DRD", "HAL", "KALU", "PVR", "RRC", "USU", "GEVO", "GMO", "EXK", "MUR", "FPP", "MOS", "REN", "SXT", "UGP", "CHKR", "POL", "WHZ", "WLK", "PGRX", "SYT", "URZ", "CLMT", "GSI", "GMET", "USAC", "CNX", "ZN", "CVX", "LLEN", "APA", "TC", "CERP", "SSRI", "EC", "LGP", "MUX", "ALJ", "PAA", "AREX", "XTEX", "PX", "SEP", "CLF", "DRQ", "GSS", "FGP", "AZC", "AA", "SVBL", "OIS", "HNRG", "TNH", "NCQ", "EXH", "PURE", "FMC", "LNDC", "PDS", "BKEP", "AXLL", "BTE", "RIOM", "SHLM", "SCOK", "VALE", "KOS", "WG", "WNRL", "YPF", "LINE", "XOM", "SXCP", "XRA", "YZC", "LSG", "TCK", "EMES", "GLF", "HSC", "CGR", "CRK", "TAS", "CAM", "YONG", "EGO", "HL", "PENX", "BBL", "MDR", "AXX", "GSE", "EPL", "FTK", "GPRE", "GST", "AAV", "EGY", "KMR", "CE", "TLR", "BAS", "TSO", "CNQ", "FUL", "PBR-A", "PSXP", "DVR", "CEQP", "APD", "PHX", "CCJ", "PGH", "SARA", "DEJ", "CGG", "SM", "GSV", "SSN", "KMG", "ROYL", "EEQ", "NL", "SSLT", "WPX", "EPD", "SVM", "DK", "PACD", "PTR", "FANG", "SYNL", "NOG", "PAGP", "SEMG", "TTI", "XEC", "EGN", "TGD", "BPZ", "WGP", "EOG", "EXXI", "URG", "ERF", "NEM", "AMCF", "ATW", "AWC", "SGY", "EEP", "MTRN", "EMXX", "OMG", "TLP", "PPG", "EGI", "TLLP", "TRX", "QRE", "AXU", "HBM", "BBG", "TORM", "IPI", "SQM", "CHK", "RES", "GOLD", "HES", "MCP", "PDH", "SZYM", "HERO", "DNR", "AHGP", "SUTR", "UAN", "VET", "REGI", "UPL", "AGU", "OXY", "WNR", "BHP", "NGLS", "BBEP", "SN", "SIM", "HFC", "SAND", "WTI", "HMY", "KGC", "WMB", "E", "PTEN", "KWK", "CAK", "KMI", "FES", "IIIN", "NEU", "ISRL", "GPL", "HUSA", "AUY", "JONE", "PSX", "OILT", "MCEP", "MT", "PBA", "QRM", "HGT", "FTI", "REE", "LXU", "ACET", "ARSD", "FF", "SU", "TRGP", "ORIG", "BCPC", "WES", "TOT", "AG", "PER", "ROC", "CHNR", "TGE", "FNV", "KOP", "VAL", "ASH", "AUQ", "NSU", "SYNM", "SFY", "MHR", "BWP", "OMN", "STO", "CF", "MVO", "TDW", "CGA", "INT", "WDFC", "CWEI", "GBR", "NRP", "SDT", "OKS", "SHW", "IAG", "SPN", "USAP", "APAGF", "NDRO", "SDR" ] }
{ "_id" : "Services", "stocks" : [ "NED", "BDL", "HTSI", "LGF", "RBA", "RLOG", "ESSX", "ATHN", "SBLK", "BLC", "CMCSA", "DRII", "HTLD", "ICFI", "JRN", "URI", "ABC", "RYAAY", "TRN", "BONA", "NCI", "LIN", "CORE", "TEU", "TMNG", "UNTK", "DL", "CTRP", "ISCA", "CP", "MW", "FIVE", "FTD", "MCK", "OCR", "UWN", "YUM", "DDE", "MHFI", "CNYD", "DDS", "NEWP", "INTX", "ORLY", "SFXE", "ETM", "SHOS", "SIX", "UNTD", "WPO", "NXST", "PTSI", "BAGR", "GMT", "AVT", "GPC", "RCL", "WSO", "SWY", "GTN", "CEA", "WLFC", "DM", "IDI", "NATH", "DOVR", "CLUB", "MATX", "TITN", "PBY", "JWN", "WEST", "DXM", "BGI", "NSC", "SAVE", "CIDM", "SFE", "GLP", "EXPD", "MCOX", "LFL", "CNCO", "HCSG", "UAL", "ARW", "APOL", "HTZ", "FREE", "HMIN", "RUK", "CAH", "ISH", "TJX", "TNP", "ZLC", "ACTG", "SUSP", "CBRL", "NAFC", "ARDNA", "PDII", "ICLD", "STON", "CBK", "CHDN", "CECO", "LPX", "OMC", "SHLD", "GEO", "SGA", "LRN", "NSSC", "RENN", "SFN", "MWIV", "AAP", "XOXO", "XWES", "PRXI", "OMX", "WYY", "RCII", "AMCO", "MCRI", "CNK", "YRCW", "RCMT", "HSON", "PTRY", "INUV", "CTAS", "HIL", "MGA", "GDOT", "FDX", "AMCN", "KKD", "SNX", "TAX", "TXRH", "PTNT", "VITC", "SNI", "ROIA", "WAGE", "DISH", "WYNN", "CMG", "TNK", "CRAI", "WAIR", "SPLS", "VLCCF", "ODP", "FORR", "AEY", "KORS", "CCL", "LUX", "PZZI", "DPZ", "CIX", "NDLS", "CCO", "MGM", "LAMR", "CATM", "SFM", "TIF", "HTHT", "HA", "ARKR", "CRV", "KFY", "QUNR", "RAD", "RMKR", "LOJN", "SAH", "TYC", "CPLA", "ABFS", "GRAM", "LAS", "HOTR", "NTN", "USAK", "ZUMZ", "DRI", "MCS", "WMAR", "FSTR", "LAWS", "STRA", "NGVC", "DSW", "RLH", "CVG", "HSIC", "WSTG", "DLHC", "PSO", "DIS", "BASI", "EBAY", "CST", "OUTR", "ARC", "BKE", "CTCM", "CACI", "ENOC", "FRED", "ESI", "FRGI", "BAH", "GMLP", "MAN", "MGT", "NSP", "ALK", "MNI", "PAYX", "PRGX", "RNDY", "SBAC", "SGRP", "SYY", "CBS", "MSM", "VVTV", "WAB", "RPXC", "TUMI", "BLDR", "AXE", "SCI", "HDSN", "VISN", "CPLP", "HMSY", "AMZN", "CCSC", "BLMN", "FURX", "ASFI", "JOSB", "EEFT", "LOV", "BIG", "ARII", "LUB", "MPEL", "MEG", "MSG", "PACR", "PTEK", "SSW", "ADT", "AH", "BKW", "SEAS", "SCHL", "CTRN", "CPHC", "BKS", "PZZA", "CMRE", "CAR", "GLNG", "QLTY", "FLL", "THI", "GPI", "EGLE", "ENL", "AHC", "MELI", "TGH", "XUE", "UHAL", "VLRS", "FRAN", "IPG", "ITRN", "FDO", "CKH", "HOT", "PNK", "CAP", "MMYT", "NYT", "RENT", "VSEC", "BXC", "HCKT", "JOBS", "VVI", "WNS", "LVNTA", "DIT", "HVT", "DAC", "GPX", "GTIM", "P", "BODY", "PENN", "NWSA", "CONN", "MNDL", "KNX", "DEST", "BOBE", "NMM", "MMS", "NFLX", "LSTR", "BLOX", "VCI", "HPOL", "EDU", "RH", "STNR", "BCO", "BPI", "CLCT", "WMT", "GSOL", "WTSL", "KR", "SBGI", "ANF", "SUSS", "ARO", "TUC", "WSTC", "SCOR", "CRMT", "SIRI", "MDCA", "PRGN", "WPPGY", "CHTR", "PRIS", "NAT", "HWCC", "FUEL", "LPS", "GNC", "CHDX", "ASCMA", "MIC", "SJR", "ENV", "KFRC", "SFL", "UNFI", "ABM", "VIFL", "JOB", "ODFL", "ABCD", "PMC", "WOOF", "DIAL", "PRTS", "ROL", "CCRN", "FRM", "NRCIB", "SB", "FWM", "GPS", "INWK", "MATW", "CEC", "MCD", "BJRI", "GCO", "LTRE", "SPCHB", "DSS", "CKEC", "EXLS", "HSNI", "CRRC", "AN", "HSII", "JBLU", "LIME", "EPAX", "LIOX", "DISCA", "MAGS", "RAIL", "LITB", "PLCE", "CVGI", "GCI", "RJET", "AL", "MRTN", "SIG", "GME", "ATSG", "CASS", "SVU", "TBI", "MSO", "OMI", "GAIA", "R", "HGG", "LEE", "BID", "GOL", "HURN", "CMLS", "DAL", "NTSC", "ABCO", "DLX", "MYGN", "NILE", "DLTR", "OWW", "PAC", "LVS", "SFLY", "EVI", "EXPE", "BRS", "HPY", "STNG", "CAKE", "LINC", "MDP", "NETC", "RLD", "TRK", "CASY", "ELRC", "JNY", "TTS", "AMCX", "TW", "BBY", "III", "PNRA", "TWC", "UTIW", "TK", "WINA", "CHEF", "LABL", "GASS", "CNI", "DIN", "DENN", "DSX", "DHX", "HAIN", "PBPB", "JW-A", "AAN", "FCN", "AZO", "CDW", "KEX", "CPA", "COSI", "IMAX", "ISLE", "GMAN", "BAGL", "RT", "IKGH", "ERS", "FINL", "PRAA", "ALCS", "SALM", "DJCO", "SCVL", "TA", "BWL-A", "GWR", "VSR", "DRYS", "BEBE", "DTV", "KIRK", "AIR", "BBSI", "EXPR", "ASEI", "WEN", "CRWN", "WWE", "OSTK", "HIBB", "INOC", "JMBA", "G", "MLNK", "MOC", "MHH", "CNW", "CRVP", "OMAB", "PFSW", "H", "MWW", "NAUH", "SBH", "MTN", "SINO", "CZR", "NM", "RUSHB", "LPSN", "DVD", "PETM", "SKS", "DANG", "SSI", "CBZ", "MGAM", "CSX", "BGFV", "STRZA", "SWFT", "CARB", "FUN", "QUAD", "RHI", "SSP", "TESS", "UPS", "RGS", "DGSE", "PDCO", "BONT", "CGX", "HZO", "FORD", "EDMC", "MED", "TAST", "TIVO", "FOXA", "RGC", "TWX", "UNP", "VALV", "WMK", "SHIP", "GFN", "YUME", "CHRM", "HHS", "PSUN", "UPG", "WLDN", "CVS", "PWX", "WTW", "BH", "VIPS", "SPDC", "WYN", "MG", "VRSK", "PCMI", "TECD", "PHII", "LYV", "DXLG", "DFRG", "RLOC", "FC", "DKS", "CODI", "RLJE", "PERF", "M", "PCLN", "BYI", "IM", "FLWS", "IILG", "ADS", "CVC", "GWW", "CBD", "GBX", "DHT", "GLBS", "CKP", "BBGI", "CHH", "RRD", "SRT", "PTSX", "TRI", "BSI", "DXPE", "MNTG", "DAVE", "IHG", "OEH", "SCIL", "ENG", "KUTV", "JEC", "KSU", "ABG", "ONVI", "ARCO", "EXAM", "REIS", "DEG", "VAC", "VLGEA", "WEX", "CUK", "FLT", "DWA", "EAT", "HAST", "AIRT", "LAD", "SMRT", "SONC", "UEPS", "MANU", "NWY", "CNTY", "TGP", "USTR", "WAG", "HRB", "ATAI", "ATV", "EDUC", "NPD", "PATR", "QKLS", "LL", "TTEC", "VSI", "CJJD", "CAB", "LBTYA", "TWMC", "MUSA", "RMGN", "ACM", "BAMM", "NTRI", "OMEX", "DCIN", "RLGT", "RSH", "LACO", "TOPS", "URS", "ZAGG", "MCO", "UACL", "EDG", "LUV", "LMCA", "NNA", "ONE", "TV", "YOD", "ZNH", "BWLD", "BWS", "CGI", "CDII", "PSMT", "LTM", "CEB", "UTI", "RRTS", "WCC", "KONA", "ASGN", "AAWW", "RADA", "RECN", "SBSA", "GSH", "CACH", "FWRD", "RRGB", "PFMT", "SBUX", "ANN", "STEI", "ASC", "PAG", "STN", "TAIT", "ROIAK", "APEI", "CCGM", "BALT", "AIT", "LCC", "IRG", "ULTA", "CETV", "AEO", "FTDDV", "CNSI", "CSS", "NEWL", "ROST", "JBHT", "KSS", "ALGT", "FRO", "CHUY", "EXPO", "DG", "GES", "DGIT", "COCO", "GNK", "HOLL", "BBW", "CVO", "ASR", "CATO", "FLY", "HUBG", "HD", "AYR", "BBBY", "BBRG", "BDMS", "MHGC", "CRRS", "DV", "MCHX", "PIR", "RTI", "SED", "NCMI", "SGMS", "CALI", "HDS", "KNOP", "SSTK", "STMP", "EZPW", "LOPE", "DLIA", "TGT", "CHKE", "RICK", "NYNY", "FRS", "CTP", "DCIX", "SPRO", "COST", "BYD", "PCCC", "TCS", "TOO", "TTEK", "UUU", "ULTR", "VSCP", "EVC", "TLYS", "WACLY", "WFM", "GCFB", "CDI", "RDI", "TISI", "USAT", "VALU", "FISV", "JCP", "VIAB", "CXW", "VPRT", "BURL", "GPN", "EGL", "GSL", "BIDZ", "KMX", "LINTA", "MM", "VTNC", "WERN", "XPO", "WSM", "CTCT", "ESEA", "TSCO", "XRS", "STB", "CEDU", "QGEN", "GK", "MGRC", "CTHR", "KTOS", "IMKTA", "SPTN", "AER", "CVTI", "KBR", "NEWT", "RUTH", "LOW", "KAR", "TAL", "ISIG", "ASNA", "SAIA", "JACK", "URBN", "ECHO", "CHS", "DNKN", "CHRW", "POWR", "LQDT", "VNTV", "CAST", "CNR", "AFCE", "RELL", "SALE", "SKYW", "STAN", "CPRT", "GLOG", "MAR", "MYCC", "PRLS", "TFM", "LTD", "EMMS", "ACY", "BFAM", "ERA", "TMH", "TUES", "KELYA", "CSV", "VOXX" ] }
...
```

## Exercício 4 - Fraude na Enron!

**1. Liste as pessoas que enviaram e-mails (de forma distinta, ou seja, sem repetir). Quantas pessoas são?**
```
db.enron.aggregate([{$group: {_id: "$sender"}}, {$count: "qtd pessoas distintas que enviaram e-mail"}])
```
```
{ "qtd pessoas distintas que enviaram e-mail" : 2200 }
```

**2. Contabilize quantos e-mails tem a palavra “fraud”**
```
db.enron.find({$or: [{text: /fraud/}, {subject: /fraud/}]}).count()
```
```
23
```
