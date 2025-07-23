import axios from 'axios';

const api = axios.create( {
    baseURL: 'https://uefachampionsleaguedrawsimulation-production-319a.up.railway.app/api',
    headers: {
        'Content-Type': 'application/json',
        
    }
});

export const teamsApi = {
    getAllTeams:() => api.get('/TeamsAPI'),

    getTeamById: (id) => api.get(`/TeamsAPI/${id}`),

    getTeamByName: (name) => api.get(`/TeamsAPI/name/${name}`),

};

export const tournamentApi = {
    drawMatches: (teamId) => api.post(`/TournamentAPI/draw?teamId=${teamId}`),
};