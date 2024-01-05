import { Fragment, useEffect, useState } from 'react'
import axios from 'axios';
import { Container, Header, List } from 'semantic-ui-react';
import { Activity } from './models/activity';
import NavBar from './NavBar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';


function App() {
  const [activities, setActivities] = useState<Activity[]>([]);
  const [selectedActivity, setSelectedActivity] = useState<Activity | undefined>(undefined);
  const [editMode, setEditMode] = useState(false);


  useEffect(() => {
    axios.get<Activity[]>('http://localhost:5000/api/activities')
      .then(response =>{
        setActivities(response.data)
      })
  },[])

  function handleSelectedActivity(id:string){
    setSelectedActivity(activities.find(x=>x.id === id))
  }

  function handleCancelSelectedActivity(){
    setSelectedActivity(undefined);
  }

  function handeFormOpen(id?:string){
    id ? handleSelectedActivity(id) : handleCancelSelectedActivity();
    setEditMode(true);
  }

  function handleFormClose(){
    setEditMode(false);
  }

  return (
    
    <>
      <NavBar openForm={handeFormOpen}/>
      <Container style={{marginTop:'7em'}}>
        <ActivityDashboard 
        activities={activities}
        selectedActivity={selectedActivity}
        selectActivity={handleSelectedActivity}
        cancelSelectActivity={handleCancelSelectedActivity}
        editMode={editMode}
        openForm={handeFormOpen}
        closeForm={handleFormClose}
        />
      </Container>
    </>
      
      
    
  )
}

export default App
