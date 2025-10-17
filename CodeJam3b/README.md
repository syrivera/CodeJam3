
# Letterbox 2.0

Minimal ASP.NET Core web app for tracking movies, users, ratings, and lists. Uses PostgreSQL and Razor Pages.

## Sample PostgreSQL Commands
Connect to your database:
```powershell
psql letterbox
```
List all tables:
```sql
\dt
```
Display first 5 users:
```sql
SELECT * FROM users LIMIT 5;
```
Display all movies in a genre:
```sql
SELECT * FROM movies WHERE genre = 'drama';
```
Show all ratings for a user:
```sql
SELECT * FROM ratings WHERE user_id = 'sam';
```
Count movies:
```sql
SELECT COUNT(*) FROM movies;
```

## Usage
- Browse movies, users, ratings, watchlists, favorites, diaries.
- Add movies to your watchlist, log movies as watched, and rate them using the web app.
- View other users' watches in PostgreSQL. Check it out!


