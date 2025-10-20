
<div align="center">
  <img src="https://a.ltrbxd.com/logos/letterboxd-logo-h-pos-rgb-1000px.png" alt="Letterboxd" width="200"/>
  <br>
  <sub>follow me <a href="https://letterboxd.com/samsclub1/">@samsclub1</a></sub>
</div>


## Letterbox 2.0

ASP.NET Core web app for tracking movies, users, ratings, and lists. Uses PostgreSQL and Razor Pages.

## How to Run the Web App
1. Make sure PostgreSQL is running and the database `letterbox` exists.
2. Restore packages, apply migrations, build, and run:
	```powershell
	dotnet ef database update
	dotnet build
	dotnet run OR dotnet watch
	```
3. Open your browser and go to:
	- `http://localhost:5147/Dashboard` (main dashboard view)
	- `http://localhost:5147/Login` (login view)
	- `http://localhost:5147/Profile` (user profile)
	- `http://localhost:5147/Movie` (movie details)

## View Options
- Dashboard: Overview of movies, users, ratings, lists
- AllTables: Raw data from all tables
- Login: Log in as a demo user
  	- (AVAILABLE USERS: "moviebuff123", "cinephile", "filmfanatic")
- Profile: View user details and lists
- Movie: See details and ratings for a movie

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
![e4200603-3756-48e2-b6c0-a7fd42243483](https://github.com/user-attachments/assets/dd29d815-2c28-4f08-ad05-2c29867e4cb6)




## Usage
- Browse movies, users, ratings, watchlists, favorites, diaries.
- Add movies to your watchlist, log movies as watched, and rate them using the web app.
- View other users' watches in PostgreSQL.


