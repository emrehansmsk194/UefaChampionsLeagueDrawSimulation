import { use, useState } from 'react'
import { useEffect } from 'react';
import Teams from './components/Teams'
import './App.css'
import { teamsApi } from './services/apiService';
import { tournamentApi } from './services/apiService';
import Matches from './components/Matches';
import React from 'react'


function App() {

const [teams, setTeams] = useState([]);
const [selectedTeamId, setSelectedTeamId] = useState(null);
const [selectedStage, setSelectedStage] = useState(0);
const [draw, setDraw] = useState(false);

useEffect(() => {
  fetchAllTeams();
}, []);


const fetchAllTeams = async () => {
  try {
    const response = await teamsApi.getAllTeams();
    setTeams(response.data);
  } catch (error) {
    console.error("Error fetching teams:", error);
  }
}
 const filteredTeams = teams.filter(team => team.stage === selectedStage);

  return (
    <>
    <div className="teams-container">
      <Teams stageNum={1} />
      <Teams stageNum={2} />
      <Teams stageNum={3} />
      <Teams stageNum={4} />
    </div>
   <div className="stage-container">
    <label for="select-stage" style={{fontSize: '1.5rem'}}>CHOOSE A POT:</label>
    <select id="select-stage" style={{width: '200px', height: '30px', fontSize: '1rem', marginLeft: '10px'}} onChange={(e) => {
       const newStage = parseInt(e.target.value);
       setSelectedStage(newStage);
       setSelectedTeamId(null);
    }}>
      <option value="0">Select a pot</option>
      <option value="1">POT 1</option>
      <option value="2">POT 2</option>
      <option value="3">POT 3</option>
      <option value="4">POT 4</option>

 
    </select>

   
   </div>
 { selectedStage != 0 &&
  <>
    <div className='selectedTeam-container'>
    <label for="select-team" style={{fontSize: '1.5rem'}}>CHOOSE TEAM:</label>
    <select id='select-team' style={{width: '200px' , height: '30px', fontSize: '1rem', marginLeft: '10px'}} value={selectedTeamId || ""} onChange = {(e) => {
      const teamId = e.target.value;
      setSelectedTeamId(teamId);
      setDraw(false);
      console.log("Selected Team ID:", teamId);
    }}>
      <option value="">Select a team</option>
      {filteredTeams.map((team,index) => (
        <option key={index} value={team.id}>
          {team.name}
        </option>
      ))}
    </select>
    
   </div>
   { !draw &&
   <div className = "button-container">
    <button className ="button-style" onClick= {() => setDraw(true)}>Draw Matches</button>
   </div>
}
   </>
 
   
}
{
  draw &&
  <>
    <div className='button-container'>
  <button className='button-style' style={{backgroundColor: 'red'}} onClick={() => setDraw(false)}>Reset Draw</button>
  </div>
   <Matches selectedTeamId={selectedTeamId} />
  
   </>
 
}

    </>
  )
}

export default App
