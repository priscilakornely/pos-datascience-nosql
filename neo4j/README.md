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
MATCH (m:Movie {released: 1992}) RETURN m.title AS Title
```

**2.5: Display title, released, and tagline values for every Movie node in the graph**
```
MATCH (m:Movie) RETURN m.title AS Title, m.released AS Release, m.tagline AS Tagline
```

**2.6: Display more user-friendly headers in the table**
```
MATCH (m:Movie) RETURN m.title AS Title, m.released AS Release, m.tagline AS Tagline
```

## Exercício 3 - Filtering queries using relationships 

**3.1: Display the schema of the database**
```
CALL db.schema.visualization()
```

**3.2: Retrieve all people who wrote the movie Speed Racer**
```
MATCH (p:Person)-[:WROTE]->(:Movie {title: 'Speed Racer'}) RETURN p.name AS Writer
```

**3.3: Retrieve all movies that are connected to the person, Tom Hanks**
```
MATCH (m:Movie)<--(:Person {name: 'Tom Hanks'}) RETURN m.title AS Title
```

**3.4: Retrieve information about the relationships Tom Hanks had with the set of movies retrieved earlier**
```
MATCH (m:Movie)-[rel]-(:Person {name: 'Tom Hanks'}) RETURN m.title AS Title, type(rel) AS Relationship
```

**3.5: Retrieve information about the roles that Tom Hanks acted in**
```
MATCH (:Person {name: 'Tom Hanks'})-[rel:ACTED_IN]-(m:Movie) RETURN m.title AS Title, rel.roles AS Roles
```

## Exercício 4 – Filtering queries using WHERE clause 

**4.1: Retrieve all movies that Tom Cruise acted in**
```
MATCH (p:Person)-[:ACTED_IN]->(m:Movie) WHERE p.name = 'Tom Cruise' RETURN m.title AS Title
```

**4.2: Retrieve all people that were born in the 70’s**
```
MATCH (p:Person) WHERE p.born >= 1970 AND p.born < 1980 RETURN p.name AS Name, p.born AS Born
```

**4.3: Retrieve the actors who acted in the movie The Matrix who were born after 1960**
```
MATCH (p:Person)-[:ACTED_IN]->(m:Movie) WHERE m.title = 'The Matrix' AND p.born > 1960 RETURN p.name AS Actor, p.born AS Born
```

**4.4: Retrieve all movies by testing the node label and a property**
```
MATCH (m) WHERE m:Movie AND m.released = 1992 RETURN m.title AS Title
```

**4.5: Retrieve all people that wrote movies by testing the relationship between two nodes**
```
MATCH (p:Person)-[rel]->(m:Movie) WHERE type(rel) = 'WROTE' RETURN p.name AS Writer, m.title AS Title
```

**4.6: Retrieve all people in the graph that do not have a property**
```
MATCH (p:Person) WHERE NOT exists(p.bithPlace) RETURN p.name AS Name
```

**4.7: Retrieve all people related to movies where the relationship has a property**
```
MATCH (p:Person)-[rel]->(m:Movie) WHERE exists(rel.roles) RETURN rel.roles AS Role, p.name AS Name, m.title AS Title
```

**4.8: Retrieve all actors whose name begins with James**
```
MATCH (p:Person)-[:ACTED_IN]->(:Movie) WHERE p.name STARTS WITH 'James' RETURN DISTINCT p.name AS Name
```

**4.9: Retrieve all REVIEW relationships from the graph with filtered results**
```
MATCH (p:Person)-[r:REVIEWED]->(:Movie) WHERE p.name STARTS WITH 'James' RETURN p.name AS Name, r.rating AS Rating, r.summary AS Review
```

**Retrieve all movies in the database that have love in their tagline and return the movie titles**
```
MATCH (m:Movie) WHERE exists(m.tagline) AND toLower(m.tagline) CONTAINS 'love' RETURN m.tagline AS Tagline, m.title AS Title
```

**Retrieve movies in the database, specifying a regular expression for the content of the tagline**
```
MATCH (m:Movie) WHERE exists(m.tagline) AND m.tagline =~ '.*life there.*' RETURN m.tagline AS Tagline, m.title AS Title
```

**4.10: Retrieve all people who have produced a movie, but have not directed a movie**
```
MATCH (p:Person)-[:PRODUCED]->(m:Movie) WHERE NOT ((p)-[:DIRECTED]->(:Movie)) RETURN p.name AS Name, m.title AS Title
```

**4.11: Retrieve the movies and their actors where one of the actors also directed the movie**
```
MATCH (p1:Person)-[:ACTED_IN]->(m:Movie)<-[:ACTED_IN]-(p2:Person) WHERE exists((p2)-[:DIRECTED]->(m)) RETURN m.title AS Title, p1.name AS Actor, p2.name AS `Actor/Director`
```

**4.12: Retrieve all movies that were released in a set of years**
```
MATCH (m:Movie) WHERE m.released in [1990, 1992, 1998, 2003] RETURN m.title AS Title, m.released AS Release
```

**Exercise 4.13: Retrieve the movies that have an actor’s role that is the name of the movie**
```
MATCH (p:Person)-[r:ACTED_IN]->(m:Movie) WHERE m.title in r.roles RETURN p.name AS Actor, r.roles AS Roles, m.title AS Title
```

## Exercício 5 – Controlling query processing

**5.1: Retrieve data using multiple MATCH patterns**
```
MATCH (w:Person)-[:WROTE]->(m:Movie), (d:Person)-[:DIRECTED]->(m) RETURN m.title AS Title, w.name AS Writer, d.name AS Director
```

**5.2: Retrieve particular nodes that have a relationship**
```
MATCH (p1:Person)-[:FOLLOWS]-(p2:Person) WHERE p1.name = 'Paul Blythe' RETURN p1, p2
```

**5.3: Modify the query to retrieve nodes that are exactly three hops away**
```
MATCH (p1:Person)-[:FOLLOWS*3]-(p2:Person) WHERE p1.name = 'Paul Blythe' RETURN p1, p2
```

**5.4: Modify the query to retrieve nodes that are one and two hops away**
```
MATCH (p1:Person)-[:FOLLOWS*1..2]-(p2:Person) WHERE p1.name = 'Paul Blythe' RETURN p1, p2
```

**5.5: Modify the query to retrieve particular nodes that are connected no matter how many hops are required**
```
MATCH (p1:Person)-[:FOLLOWS*]-(p2:Person) WHERE p1.name = 'Paul Blythe' RETURN p1, p2
```

**5.6: Specify optional data to be retrieved during the query**
```
MATCH (p:Person) WHERE p.name STARTS WITH 'James' OPTIONAL MATCH (p)-[r:REVIEWED]->(m:Movie) RETURN p.name AS Name, m.title AS Title
```

**5.7: Retrieve nodes by collecting a list**
```
MATCH (p:Person)-[:DIRECTED]->(m:Movie) RETURN p.name as Director, COLLECT(m.title) AS `Movie List`
```

**5.8: Retrieve all movies that Tom Cruise has acted in and the co-actors that acted in the same movie by collecting a list**
```
MATCH (p:Person)-[:ACTED_IN]->(m:Movie)<-[:ACTED_IN]-(p2:Person) WHERE p.name ='Tom Cruise' RETURN m.title AS Title, COLLECT(p2.name) AS `Co-Actors`
```

**5.9: Retrieve nodes as lists and return data associated with the corresponding lists**
```
MATCH (p:Person)-[:ACTED_IN]->(m:Movie) RETURN m.title AS Title, COUNT(p) AS `Number Actors`, COLLECT(p.name) AS Actors
```

**5.10: Retrieve nodes and their relationships as lists**
```
MATCH (d:Person)-[:DIRECTED]->(m:Movie)<-[:ACTED_IN]-(a:Person) RETURN d.name AS Director, COUNT(a) AS `Numer Actors`, COLLECT(a.name) AS Actors
```

**5.11: Retrieve the actors who have acted in exactly five movies**
```
MATCH (a:Person)-[:ACTED_IN]->(m:Movie) WITH a, COUNT(a) AS numMovies, COLLECT(m.title) AS movies WHERE numMovies = 5 RETURN a.name AS Actor, movies
```

**5.12: Retrieve the movies that have at least 2 directors with other optional data**
```
MATCH (m:Movie) WITH m, SIZE((:Person)-[:DIRECTED]->(m)) AS directors WHERE directors >= 2 OPTIONAL MATCH (p:Person)-[:REVIEWED]->(m) RETURN  m.title AS Title, p.name AS Reviewer
```

## Exercício 6 – Controlling results returned

**6.1: Execute a query that returns duplicate records**
```
MATCH (r:Person)-[:REVIEWED]->(m:Movie) RETURN DISTINCT m.released AS Release, m.title AS Title, COLLECT(r.name) AS Reviwers
```

**6.2: Modify the query to eliminate duplication**
```
MATCH (r:Person)-[:REVIEWED]->(m:Movie) RETURN DISTINCT m.released AS Release, COLLECT(m.title) AS Titles, COLLECT(r.name) AS Reviwers
```

**6.3: Modify the query to eliminate more duplication**
```
MATCH (r:Person)-[:REVIEWED]->(m:Movie) RETURN DISTINCT m.released AS Release, COLLECT(DISTINCT m.title) AS Titles, COLLECT(r.name) AS Reviwers
```

**6.4: Sort results returned**
```
MATCH (r:Person)-[:REVIEWED]->(m:Movie) RETURN DISTINCT m.released AS Release, COLLECT(DISTINCT m.title) AS Titles, COLLECT(r.name) AS Reviwers ORDER BY m.released
```

**6.5: Retrieve the top 5 ratings and their associated movies**
```
MATCH (:Person)-[r:REVIEWED]->(m:Movie) RETURN m.title AS Title, r.rating AS Rating ORDER BY r.rating DESC LIMIT 5
```

**6.6: Retrieve all actors that have not appeared in more than 3 movies**
```
MATCH (a:Person)-[:ACTED_IN]->(m:Movie) WITH a, COUNT(a) AS numMovies, COLLECT(m.title) AS movies WHERE numMovies <= 3 RETURN a.name AS Actor, movies
