# Neo4j

## Exercício 1 - Retrieving Nodes

**1.1: Retrieve all nodes from the database**
```
MATCH (n)
RETURN n
```

**1.2: Examine the data model for the graph**
```
CALL db.schema.visualization()
```

**1.3: Retrieve all Person nodes**
```
MATCH (p:Person)
RETURN p
```

**1.4: Retrieve all Movie nodes**
```
MATCH (m:Movie)
RETURN m
```

## Exercício 2 – Filtering queries using property values

**2.1: Retrieve all movies that were released in a specific year**
```
MATCH (m:Movie {released:1992})
RETURN m
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
MATCH (m:Movie {released: 1992})
RETURN m.title AS Title
```

**2.5: Display title, released, and tagline values for every Movie node in the graph**
```
MATCH (m:Movie)
RETURN m.title AS Title, m.released AS Release, m.tagline AS Tagline
```

**2.6: Display more user-friendly headers in the table**
```
MATCH (m:Movie)
RETURN m.title AS Title, m.released AS Release, m.tagline AS Tagline
```

## Exercício 3 - Filtering queries using relationships 

**3.1: Display the schema of the database**
```
CALL db.schema.visualization()
```

**3.2: Retrieve all people who wrote the movie Speed Racer**
```
MATCH (p:Person)-[:WROTE]->(:Movie {title: 'Speed Racer'})
RETURN p.name AS Writer
```

**3.3: Retrieve all movies that are connected to the person, Tom Hanks**
```
MATCH (m:Movie)<--(:Person {name: 'Tom Hanks'})
RETURN m.title AS Title
```

**3.4: Retrieve information about the relationships Tom Hanks had with the set of movies retrieved earlier**
```
MATCH (m:Movie)-[rel]-(:Person {name: 'Tom Hanks'})
RETURN m.title AS Title, type(rel) AS Relationship
```

**3.5: Retrieve information about the roles that Tom Hanks acted in**
```
MATCH (:Person {name: 'Tom Hanks'})-[rel:ACTED_IN]-(m:Movie)
RETURN m.title AS Title, rel.roles AS Roles
```

## Exercício 4 – Filtering queries using WHERE clause 

**4.1: Retrieve all movies that Tom Cruise acted in**
```
MATCH (p:Person)-[:ACTED_IN]->(m:Movie)
WHERE p.name = 'Tom Cruise'
RETURN m.title AS Title
```

**4.2: Retrieve all people that were born in the 70’s**
```
MATCH (p:Person)
WHERE p.born >= 1970 AND p.born < 1980
RETURN p.name AS Name, p.born AS Born
```

**4.3: Retrieve the actors who acted in the movie The Matrix who were born after 1960**
```
MATCH (p:Person)-[:ACTED_IN]->(m:Movie)
WHERE m.title = 'The Matrix' AND p.born > 1960
RETURN p.name AS Actor, p.born AS Born
```

**4.4: Retrieve all movies by testing the node label and a property**
```
MATCH (m)
WHERE m:Movie AND m.released = 1992
RETURN m.title AS Title
```

**4.5: Retrieve all people that wrote movies by testing the relationship between two nodes**
```
MATCH (p:Person)-[rel]->(m:Movie)
WHERE type(rel) = 'WROTE'
RETURN p.name AS Writer, m.title AS Title
```

**4.6: Retrieve all people in the graph that do not have a property**
```
MATCH (p:Person)
WHERE NOT exists(p.bithPlace)
RETURN p.name AS Name
```

**4.7: Retrieve all people related to movies where the relationship has a property**
```
MATCH (p:Person)-[rel]->(m:Movie)
WHERE exists(rel.roles)
RETURN rel.roles AS Role, p.name AS Name, m.title AS Title
```

**4.8: Retrieve all actors whose name begins with James**
```
MATCH (p:Person)-[:ACTED_IN]->(:Movie)
WHERE p.name STARTS WITH 'James'
RETURN DISTINCT p.name AS Name
```

**4.9: Retrieve all REVIEW relationships from the graph with filtered results**
```
MATCH (p:Person)-[r:REVIEWED]->(:Movie)
WHERE p.name STARTS WITH 'James'
RETURN p.name AS Name, r.rating AS Rating, r.summary AS Review
```

**Retrieve all movies in the database that have love in their tagline and return the movie titles**
```
MATCH (m:Movie)
WHERE exists(m.tagline) AND toLower(m.tagline) CONTAINS 'love'
RETURN m.tagline AS Tagline, m.title AS Title
```

**Retrieve movies in the database, specifying a regular expression for the content of the tagline**
```
MATCH (m:Movie)
WHERE exists(m.tagline) AND m.tagline =~ '.*life there.*'
RETURN m.tagline AS Tagline, m.title AS Title
```

**4.10: Retrieve all people who have produced a movie, but have not directed a movie**
```
MATCH (p:Person)-[:PRODUCED]->(m:Movie)
WHERE NOT ((p)-[:DIRECTED]->(:Movie))
RETURN p.name AS Name, m.title AS Title
```

**4.11: Retrieve the movies and their actors where one of the actors also directed the movie**
```
MATCH (p1:Person)-[:ACTED_IN]->(m:Movie)<-[:ACTED_IN]-(p2:Person)
WHERE exists((p2)-[:DIRECTED]->(m))
RETURN m.title AS Title, p1.name AS Actor, p2.name AS `Actor/Director`
```

**4.12: Retrieve all movies that were released in a set of years**
```
MATCH (m:Movie)
WHERE m.released in [1990, 1992, 1998, 2003]
RETURN m.title AS Title, m.released AS Release
```

**Exercise 4.13: Retrieve the movies that have an actor’s role that is the name of the movie**
```
MATCH (p:Person)-[r:ACTED_IN]->(m:Movie)
WHERE m.title in r.roles
RETURN p.name AS Actor, r.roles AS Roles, m.title AS Title
```

## Exercício 5 – Controlling query processing

**5.1: Retrieve data using multiple MATCH patterns**
```
MATCH (w:Person)-[:WROTE]->(m:Movie), (d:Person)-[:DIRECTED]->(m)
RETURN m.title AS Title, w.name AS Writer, d.name AS Director
```

**5.2: Retrieve particular nodes that have a relationship**
```
MATCH (p1:Person)-[:FOLLOWS]-(p2:Person)
WHERE p1.name = 'Paul Blythe'
RETURN p1, p2
```

**5.3: Modify the query to retrieve nodes that are exactly three hops away**
```
MATCH (p1:Person)-[:FOLLOWS*3]-(p2:Person)
WHERE p1.name = 'Paul Blythe'
RETURN p1, p2
```

**5.4: Modify the query to retrieve nodes that are one and two hops away**
```
MATCH (p1:Person)-[:FOLLOWS*1..2]-(p2:Person)
WHERE p1.name = 'Paul Blythe'
RETURN p1, p2
```

**5.5: Modify the query to retrieve particular nodes that are connected no matter how many hops are required**
```
MATCH (p1:Person)-[:FOLLOWS*]-(p2:Person)
WHERE p1.name = 'Paul Blythe'
RETURN p1, p2
```

**5.6: Specify optional data to be retrieved during the query**
```
MATCH (p:Person)
WHERE p.name STARTS WITH 'James' OPTIONAL MATCH (p)-[r:REVIEWED]->(m:Movie)
RETURN p.name AS Name, m.title AS Title
```

**5.7: Retrieve nodes by collecting a list**
```
MATCH (p:Person)-[:DIRECTED]->(m:Movie)
RETURN p.name as Director, COLLECT(m.title) AS `Movie List`
```

**5.8: Retrieve all movies that Tom Cruise has acted in and the co-actors that acted in the same movie by collecting a list**
```
MATCH (p:Person)-[:ACTED_IN]->(m:Movie)<-[:ACTED_IN]-(p2:Person)
WHERE p.name ='Tom Cruise'
RETURN m.title AS Title, COLLECT(p2.name) AS `Co-Actors`
```

**5.9: Retrieve nodes as lists and return data associated with the corresponding lists**
```
MATCH (p:Person)-[:ACTED_IN]->(m:Movie)
RETURN m.title AS Title, COUNT(p) AS `Number Actors`, COLLECT(p.name) AS Actors
```

**5.10: Retrieve nodes and their relationships as lists**
```
MATCH (d:Person)-[:DIRECTED]->(m:Movie)<-[:ACTED_IN]-(a:Person)
RETURN d.name AS Director, COUNT(a) AS `Number Actors`, COLLECT(a.name) AS Actors
```

**5.11: Retrieve the actors who have acted in exactly five movies**
```
MATCH (a:Person)-[:ACTED_IN]->(m:Movie) WITH a, COUNT(a) AS numMovies, COLLECT(m.title) AS movies
WHERE numMovies = 5
RETURN a.name AS Actor, movies
```

**5.12: Retrieve the movies that have at least 2 directors with other optional data**
```
MATCH (m:Movie) WITH m, SIZE((:Person)-[:DIRECTED]->(m)) AS directors
WHERE directors >= 2 OPTIONAL MATCH (p:Person)-[:REVIEWED]->(m)
RETURN  m.title AS Title, p.name AS Reviewer
```

## Exercício 6 – Controlling results returned

**6.1: Execute a query that returns duplicate records**
```
MATCH (r:Person)-[:REVIEWED]->(m:Movie)
RETURN DISTINCT m.released AS Release, m.title AS Title, COLLECT(r.name) AS Reviewers
```

**6.2: Modify the query to eliminate duplication**
```
MATCH (r:Person)-[:REVIEWED]->(m:Movie)
RETURN DISTINCT m.released AS Release, COLLECT(m.title) AS Titles, COLLECT(r.name) AS Reviewers
```

**6.3: Modify the query to eliminate more duplication**
```
MATCH (r:Person)-[:REVIEWED]->(m:Movie)
RETURN DISTINCT m.released AS Release, COLLECT(DISTINCT m.title) AS Titles, COLLECT(r.name) AS Reviewers
```

**6.4: Sort results returned**
```
MATCH (r:Person)-[:REVIEWED]->(m:Movie)
RETURN DISTINCT m.released AS Release, COLLECT(DISTINCT m.title) AS Titles, COLLECT(r.name) AS Reviewers
ORDER BY m.released
```

**6.5: Retrieve the top 5 ratings and their associated movies**
```
MATCH (:Person)-[r:REVIEWED]->(m:Movie)
RETURN m.title AS Title, r.rating AS Rating
ORDER BY r.rating DESC LIMIT 5
```

**6.6: Retrieve all actors that have not appeared in more than 3 movies**
```
MATCH (a:Person)-[:ACTED_IN]->(m:Movie) WITH a, COUNT(a) AS numMovies, COLLECT(m.title) AS movies
WHERE numMovies <= 3
RETURN a.name AS Actor, movies
```

## Exercício 7 – Working with cypher data

**7.1: Collect and use lists**
```
MATCH (a:Person)-[:ACTED_IN]->(m:Movie), (m)<-[:REVIEWED]-(r:Person) WITH m, COLLECT(DISTINCT a.name) AS Cast, COLLECT(DISTINCT r.name) AS Reviewers
RETURN DISTINCT m.title AS Title, Cast, Reviewers ORDER BY SIZE(Cast)
```

**7.2: Collect a list**
```
MATCH (a:Person)-[:ACTED_IN]->(m:Movie) WITH a, COLLECT(m) AS Movies
WHERE SIZE(Movies) > 3
RETURN a.name AS Actor, Movies
```

**7.3: Unwind a list**
```
MATCH (d:Person)-[:DIRECTED]->(m:Movie) WITH d, COLLECT(m) AS Movies
WHERE SIZE(Movies) > 1 WITH d, Movies UNWIND Movies AS Movie
RETURN d.name AS Director, Movie
```

**7.4: Perform a calculation with the date type**
```
MATCH (a:Person)-[:ACTED_IN]->(m:Movie)
WHERE a.name = 'Tom Hanks'
RETURN m.title AS Title, m.released AS Release, date().year - m.released AS `Years Ago Released`,
m.released - a.born AS `Age of Tom` ORDER BY `Years Ago Released`
```

## Exercício 8 - Creating nodes

**8.1: Create a Movie node**
```
CREATE (:Movie {title: 'Forrest Gump'})
```

**8.2: Retrieve the newly-created node**
```
MATCH (m:Movie)
WHERE m.title = 'Forrest Gump'
RETURN m AS Movie
```

**8.3: Create a Person node**
```
CREATE (:Person {name: 'Robin Wright'})
```

**8.4: Retrieve the newly-created node**
```
MATCH (p:Person)
WHERE p.name = 'Robin Wright'
RETURN p AS Person
```

**8.5: Add a label to a node**
```
MATCH (m:Movie)
WHERE m.released < 2015
SET m:OlderMovie
RETURN DISTINCT labels(m)
```

**8.6: Retrieve the node using the new label**
```
MATCH (m:OlderMovie)
RETURN m.title AS Title, m.released AS Release
ORDER BY Release DESC
```

**8.7: Add the Female label to selected nodes**
```
MATCH (p:Person)
WHERE p.name STARTS WITH 'Robin'
SET p:Female
```

**8.8: Retrieve all Female nodes**
```
MATCH (p:Female)
RETURN p.name AS Name
```

**8.9: Remove the Female label from the nodes that have this label**
```
MATCH (p:Female)
REMOVE p:Female
```

**8.10: View the current schema of the graph**
```
CALL db.schema.visualization()
```

**8.11: Add properties to a movie**
```
MATCH (m:Movie)
WHERE m.title = 'Forrest Gump'
SET m:OlderMovie, m.released = 1994, m.tagline = "Life is like a box of chocolates...you never know what you're gonna get.",
m.lengthInMinutes = 142
```

**8.12: Retrieve an OlderMovie node to confirm the label and properties**
```
MATCH (m:OlderMovie)
WHERE m.title = 'Forrest Gump'
RETURN m AS Movie
```

**8.13: Add properties to the person, Robin Wright**
```
MATCH (p:Person)
WHERE p.name = 'Robin Wright'
SET p.born = 1966, p.birthPlace = 'Dallas'
```

**8.14: Retrieve an updated Person node**
```
MATCH (p:Person)
WHERE p.name = 'Robin Wright'
RETURN p AS Person
```

**8.15: Remove a property from a Movie node**
```
MATCH (m:Movie)
WHERE m.title = 'Forrest Gump'
SET m.lengthInMinutes = null
```

**8.16: Retrieve the node to confirm that the property has been removed**
```
MATCH (m:Movie)
WHERE m.title = 'Forrest Gump'
RETURN m AS Movie
```

**8.17: Remove a property from a Person node**
```
MATCH (p:Person)
WHERE p.name = 'Robin Wright'
REMOVE p.birthPlace
```

**8.18: Retrieve the node to confirm that the property has been removed**
```
MATCH (p:Person)
WHERE p.name = 'Robin Wright'
RETURN p AS Person
```

## Exercício 9 - Creating relationships

**9.1: Create ACTED_IN relationships**
```
MATCH (m:Movie)
WHERE m.title = 'Forrest Gump' MATCH (p:Person)
WHERE p.name = 'Tom Hanks' OR p.name = 'Robin Wright' OR p.name = 'Gary Sinise'
CREATE (p)-[:ACTED_IN]->(m)
```

**9.2: Create DIRECTED relationships**
```
MATCH (m:Movie)
WHERE m.title = 'Forrest Gump' MATCH (p:Person)
WHERE p.name = 'Robert Zemeckis'
CREATE (p)-[:DIRECTED]->(m)
```

**9.3: Create a HELPED relationship**
```
MATCH (p1:Person)
WHERE p1.name = 'Tom Hanks' MATCH (p2:Person)
WHERE p2.name = 'Gary Sinise'
CREATE (p1)-[:HELPED]->(p2)
```

**9.4: Query nodes and new relationships**
```
MATCH (p:Person)-[rel]-(m:Movie)
WHERE m.title = 'Forrest Gump'
RETURN p AS Person, rel AS Relationship, m AS Movie
```

**9.5: Add properties to relationships**
```
MATCH (p:Person)-[rel:ACTED_IN]->(m:Movie)
WHERE m.title = 'Forrest Gump'
SET rel.roles = CASE p.name WHEN 'Tom Hanks' THEN ['Forrest Gump']
WHEN 'Robin Wright' THEN ['Jenny Curran']
WHEN 'Gary Sinise' THEN ['Lieutenant Dan Taylor'] END
```

**9.6: Add a property to the HELPED relationship**
```
MATCH (p1:Person)-[rel:HELPED]->(p2:Person)
WHERE p1.name = 'Tom Hanks' AND p2.name = 'Gary Sinise'
SET rel.research = 'war history'
```

**9.7: View the current list of property keys in the graph**
```
call db.propertyKeys
```

**9.8: View the current schema of the graph**
```
CALL db.schema.visualization()
```

**9.9: Retrieve the names and roles for actors**
```
MATCH (p:Person)-[rel:ACTED_IN]->(m:Movie)
WHERE m.title = 'Forrest Gump'
RETURN p.name AS Actor, rel.roles AS Roles
ORDER BY Actor
```

**9.10: Retrieve information about any specific relationships**
```
MATCH (p1:Person)-[rel:HELPED]-(p2:Person)
RETURN p1.name, rel, p2.name
```

**9.11: Modify a property of a relationship**
```
MATCH (p:Person)-[rel:ACTED_IN]->(m:Movie)
WHERE m.title = 'Forrest Gump' AND p.name = 'Gary Sinise'
SET rel.roles = ['Lt. Dan Taylor']
```

**9.12: Remove a property from a relationship**
```
MATCH (p1:Person)-[rel:HELPED]->(p2:Person)
WHERE p1.name = 'Tom Hanks' AND p2.name = 'Gary Sinise'
REMOVE rel.research
```

**9.13: Confirm that your modifications were made to the graph**
```
MATCH (p:Person)-[rel:ACTED_IN]->(m:Movie)
WHERE m.title = 'Forrest Gump'
RETURN p AS Person, rel AS Relationship, m AS Movie
```

## Exercício 10 - Deleting nodes and relationships

**10.1: Delete a relationship**
```
MATCH (:Person)-[rel:HELPED]-(:Person)
DELETE rel
```

**10.2: Confirm that the relationship has been deleted**
```
MATCH (:Person)-[rel:HELPED]-(:Person)
RETURN rel
```

**10.3: Retrieve a movie and all of its relationships**
```
MATCH (p:Person)-[rel]-(m:Movie)
WHERE m.title = 'Forrest Gump'
RETURN p AS Person, rel AS Relationship, m AS Movie
```

**10.4: Try deleting a node without detaching its relationships**
```
MATCH (m:Movie)
WHERE m.title = 'Forrest Gump'
DELETE m
```

**10.5: Delete a Movie node, along with its relationships**
```
MATCH (m:Movie)
WHERE m.title = 'Forrest Gump'
DETACH DELETE m
```

**10.6: Confirm that the Movie node has been deleted**
```
MATCH (p:Person)-[rel]-(m:Movie)
WHERE m.title = 'Forrest Gump'
RETURN p AS Person, rel AS Relationship, m AS Movie
```
