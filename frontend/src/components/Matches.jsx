import React, { use } from 'react'
import { useState } from 'react'
import { useEffect } from 'react'
import { useRef } from 'react'
import { tournamentApi } from '../services/apiService'
import './matches.css'


const Matches = ({selectedTeamId}) => {
const [matches, setMatches] = useState([]);
const [loading, setLoading] = useState(false);
const matchesRef = useRef(null);

useEffect(() => {
  if (selectedTeamId) {
    fetchMatches(selectedTeamId);
  }
  
}, [selectedTeamId]);

const fetchMatches = async (selectedTeamId) => {
  setLoading(true);
  try {
    const response = await tournamentApi.drawMatches(selectedTeamId);
    const fetchedMatches = response.data?.matches || [];
    const shuffledMatches = shuffleMatches(fetchedMatches);
    
    setTimeout(() => {
      setMatches(shuffledMatches);
        requestAnimationFrame(() => {
        requestAnimationFrame(() => {
          if (matchesRef.current) {
            matchesRef.current.scrollIntoView({
              behavior: 'smooth',
              block: 'start'
            });
          }
        });
      });
      
    }, 1000);
  } catch (error) {
    console.error("Error fetching matches:", error);
  } finally {
    setLoading(false);
  }
}
const shuffleMatches = (matchData) => {
  const evenIndices = [];
  const oddIndices = [];
  
  for (let i = 0; i < matchData.length; i++) {
    if (i % 2 === 0) {
      evenIndices.push(i);
    } else {
      oddIndices.push(i);
    }
  }
  
  evenIndices.sort(() => Math.random() - 0.5);
  oddIndices.sort(() => Math.random() - 0.5);
  
  const newMatches = [];
  for (let i = 0; i < matchData.length; i++) {
    if (i % 2 === 0) {
      const randomEvenIndex = evenIndices[i / 2];
      newMatches[i] = matchData[randomEvenIndex];
    } else {
      const randomOddIndex = oddIndices[Math.floor(i / 2)];
      newMatches[i] = matchData[randomOddIndex];
    }
  }
  
  return newMatches; 
}



  return (

    <div ref={matchesRef}>
      {      loading ? <div className="loader"></div> :   
            <ul>
                {matches.map((match, index) => (
                    <div key={index} className="match-container">
                        <img src={match.homeLogoUrl} alt={match.homeTeam} style={{ width: '50px', height: '50px' }} />
                       <h2 style={{fontFamily: 'Inter, sans-serif'}}>{match.homeTeam} - {match.awayTeam}</h2>
                       <img src={match.awayLogoUrl} alt={match.awayTeam} style={{ width: '50px', height: '50px' }} />
                    </div>
                ))}
            </ul>
        }
        
    </div>
    
  )
}

export default Matches