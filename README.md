## This program consists of two parts
- The console client
- The mvc presentation layer

### The console client gets the data from the computer and sends it to the mvc server through the WebSocket protocol.

### The mvc project intercepts the WebSocket request and saves it to the database, the database is a PostgreSql database with ADO.NET as the connection layer (no ORM is used).

### The final stage is the presentation layer which involves getting the data from the database and showing it to the screen.

### The MVC architectural pattern was used. Bootstrap was used to develop the UI.