# Neo4j

## Exercício 1 - Retrieving Nodes

**1.1: Retrieve all nodes from the database**
```
MATCH (n) RETURN n
```

**1.2: Examine the data model for the graph**
```
CALL db.schema.visualization()
```

**1.3: Retrieve all Person nodes**
```
MATCH (p:Person) RETURN p
```

**1.4: Retrieve all Movie nodes**
```
MATCH (m:Movie) RETURN m
```

## Exercício 2 – Filtering queries using property values

**2.1: Retrieve all movies that were released in a specific year**
```
MATCH (m:Movie {released:1992}) RETURN m
```

**2.2: View the retrieved results as a table**
```
```

**Exercise 2.3: Query the database for all property keys**
```
CALL db.propertyKeys
```

**2.4: Retrieve all Movies released in a specific year, returning their titles**
```
MATCH (m:Movie {released: 1992}) RETURN m.title AS `Título`
```

**2.5: Display title, released, and tagline values for every Movie node in the graph**
```
MATCH (m:Movie) RETURN m.title, m.released, m.tagline
```

**2.6: Display more user-friendly headers in the table**
```
MATCH (m:Movie) RETURN m.title AS `Título`, m.released AS `Lançamento`, m.tagline AS `Slogan`
```

## Exercício 3 - Filtering queries using relationships 

**3.1: Display the schema of the database**
```
CALL db.schema.visualization()
```

**3.2: Retrieve all people who wrote the movie Speed Racer**
```
MATCH (p:Person)-[:WROTE]->(:Movie {title: 'Speed Racer'}) RETURN p.name AS `Escritor`
```

**3.3: Retrieve all movies that are connected to the person, Tom Hanks**
```
MATCH (m:Movie)<--(:Person {name: 'Tom Hanks'}) RETURN m.title AS `Título`
```

**3.4: Retrieve information about the relationships Tom Hanks had with the set of movies retrieved earlier**
```
MATCH (m:Movie)-[rel]-(:Person {name: 'Tom Hanks'}) RETURN m.title AS `Título`, type(rel) AS `Relacionamento`
```

**3.5: Retrieve information about the roles that Tom Hanks acted in**
```
MATCH (:Person {name: 'Tom Hanks'})-[rel:ACTED_IN]-(m:Movie) RETURN m.title AS `Título`, rel.roles AS `Papel`
```

## Exercício 4 – Filtering queries using WHERE clause 

**4.1: Retrieve all movies that Tom Cruise acted in**
```
MATCH (p:Person)-[:ACTED_IN]->(m:Movie) WHERE p.name = 'Tom Cruise' RETURN m.title AS `Título`
```

**4.2: Retrieve all people that were born in the 70’s**
```
MATCH (p:Person) WHERE p.born >= 1970 AND p.born < 1980 RETURN p.name AS Nome, p.born AS `Ano Nascimento`
```

**4.3: Retrieve the actors who acted in the movie The Matrix who were born after 1960**
```
MATCH (p:Person)-[:ACTED_IN]->(m:Movie) WHERE p.born > 1960 AND m.title = 'The Matrix' RETURN p.name AS Nome, p.born AS `Ano Nascimento`
```

**4.4: Retrieve all movies by testing the node label and a property**
```
MATCH (m) WHERE m:Movie AND m.released = 1992 RETURN m.title AS `Título`
```

**4.5: Retrieve all people that wrote movies by testing the relationship between two nodes**
```
MATCH (p:Person)-[rel]->(m:Movie) WHERE type(rel) = 'WROTE' RETURN p.name AS `Escritor`, m.title AS `Título`
```

**4.6: Retrieve all people in the graph that do not have a property**
```
MATCH (p:Person) WHERE NOT exists(p.born) RETURN p.name AS Nome
```

**4.7: Retrieve all people related to movies where the relationship has a property**
```
MATCH (p:Person)-[rel]->(m:Movie) WHERE exists(rel.rating) RETURN rel.rating AS Rating, p.name AS Nome, m.title AS `Título`
```

**4.8: Retrieve all actors whose name begins with James**
```
MATCH (p:Person)-[:ACTED_IN]->(:Movie) WHERE p.name STARTS WITH 'James' RETURN DISTINCT p.name AS Nome
```

**4.9: Retrieve all REVIEW relationships from the graph with filtered results**
```
MATCH (p:Person)-[r:REVIEWED]->(:Movie) WHERE p.name STARTS WITH 'James' RETURN r
```

**Retrieve all movies in the database that have love in their tagline and return the movie titles**
```
MATCH (m:Movie) WHERE exists(m.tagline) AND toLower(m.tagline) CONTAINS 'love' RETURN m.tagline AS Slogan, m.title AS `Título`
```

**Retrieve movies in the database, specifying a regular expression for the content of the tagline**
```
MATCH (m:Movie) WHERE exists(m.tagline) AND m.tagline =~ '.*life there.*' RETURN m.tagline AS Slogan, m.title AS `Título`
```

**4.10: Retrieve all people who have produced a movie, but have not directed a movie**
```
MATCH (p:Person)-[:PRODUCED]->(m:Movie) WHERE NOT ((p)-[:DIRECTED]->(:Movie)) RETURN p.name AS Nome, m.title AS `Título`
```

**4.11: Retrieve the movies and their actors where one of the actors also directed the movie**
```
MATCH (p1:Person)-[:ACTED_IN]->(m:Movie)<-[:ACTED_IN]-(p2:Person) WHERE exists((p2)-[:DIRECTED]->(m)) RETURN m.title AS `Título`, p1.name AS Ator, p2.name AS `Ator/Diretor`
```

**4.12: Retrieve all movies that were released in a set of years**
```
MATCH (m:Movie) WHERE m.released in [1990, 1992, 1998, 2003] RETURN m.title AS `Título`, m.released AS `Lançamento`
```

**Exercise 4.13: Retrieve the movies that have an actor’s role that is the name of the movie**
```
MATCH (p:Person)-[r:ACTED_IN]->(m:Movie) WHERE m.title in r.roles RETURN p.name AS Ator, m.title AS `Título`
```
