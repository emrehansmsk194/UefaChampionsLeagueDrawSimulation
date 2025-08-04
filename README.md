# ⚽ UEFA Champions League Draw Simulation

## Live Demo
- **Website:** https://ucldrawsimulation.up.railway.app
- **API:** https://uefachampionsleaguedrawsimulation-production-319a.up.railway.app/api

## Screenshots
<img width="1726" height="898" alt="resim" src="https://github.com/user-attachments/assets/5beb4310-8512-4f74-bc52-cf9d6b9fd2a2" />
<img width="1749" height="840" alt="resim" src="https://github.com/user-attachments/assets/1a17cb85-8e32-4bf7-8a34-733bbc7cb023" />

## Technologies
- **Backend:** .NET 9 Web API
- **Frontend:** React
- **Database:** PostgreSQL
- **Deploy:** Railway

## API Endpoints
### Teams
```http
GET /TeamsAPI                    # Gel all Teams
GET /TeamsAPI/{id}              # Get Team by Id
GET /TeamsAPI/name/{name}       # Get Team by Name
POST /TeamsAPI                  # Add Team
PUT /TeamsAPI?id={id}           # Update Team
DELETE /TeamsAPI?id={id}        # Delete Team
```
### Tournament
```http
POST /TournamentAPI/draw?teamId={id} #Draw
```
## Features
- Real UEFA draws
- 36 Team, 4 Pot System 
- Screenshot
- Responsive Design
