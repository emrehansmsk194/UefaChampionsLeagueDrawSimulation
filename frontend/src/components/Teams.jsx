import React from 'react'
import { useState } from 'react'
import { useEffect } from 'react'
import { tournamentApi, teamsApi } from '../services/apiService'
import './teams.css'




const Teams = ({stageNum}) => {
const fetchData = async () => {
    try {
        const response = await teamsApi.getAllTeams();
        setTeams(response.data);
        console.log(response.data);

    }
    catch (error) {
        console.error("Error fetching teams:", error);
    }
}

const drawMatches = async (teamId) => {
    try {
        const response = await tournamentApi.drawMatches(teamId);
        setMatches(response.data);
    }
    catch (error) {
        console.error("Error drawing matches:", error);
    }
}
const [teams,setTeams] = useState([]);

useEffect(() => {
    fetchData();
}, []);

const filteredTeams = teams.filter(team => team.stage === stageNum);
  return (
    <div>
        <div>
            <h2 style={{textAlign: 'center'}}>POT {stageNum}</h2>
            <ul>
               {filteredTeams.map((team, index) => (
                   <div key={index} className="teamContainer">
                    <img src = {team.logoUrl} alt = {team.name} style={{ width: '50px', height: '50px', padding: '5px'}} />
                       <p style={{fontFamily: 'Inter, sans-serif'}}>{team.name}</p>
                   </div>
               ))}
            </ul>
        </div>
    </div>
  )
}

export default Teams