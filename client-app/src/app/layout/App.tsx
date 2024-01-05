import { useEffect, useState } from 'react'
import axios from 'axios';
import { Container,} from 'semantic-ui-react';
import { Activity } from './models/activity';
import NavBar from './NavBar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import {v4 as uuid} from 'uuid';
import agent from './api/agent';
import LoadingComponent from './LoadingComponent';

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);
  const [selectedActivity, setSelectedActivity] = useState<Activity | undefined>(undefined);
  const [editMode, setEditMode] = useState(false);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);


  useEffect(() => {
    agent.Activities.list()
      .then(response =>{ //data from API
        let activities: Activity[] = [];
        response.forEach(activity=>{
          activity.date = activity.date.split('T')[0];//to better format for date
          activities.push(activity);
        })
        setActivities(activities)
        setLoading(false);
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

  function handleCreateOrEditActivity(activity: Activity){
    setSubmitting(true);
    if(activity.id){
      agent.Activities.update(activity).then(()=>{
        setActivities([...activities.filter(x=>x.id !== activity.id), activity])
        setSelectedActivity(activity);
        setEditMode(false);
        setSubmitting(false);
      })
    }else{
      activity.id = uuid();
      agent.Activities.create(activity).then(()=>{
        setActivities([...activities, activity]);
        setSelectedActivity(activity);
        setEditMode(false);
        setSubmitting(false);
      })
    }
  }

  function handleDeleteActivity(id:string){
    setActivities([...activities.filter(x=>x.id !== id)])//set new activity list except the deleted one.
  }

  if(loading) return <LoadingComponent content='Loading app' />

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
        createOrEdit = {handleCreateOrEditActivity}
        deleteActivity = {handleDeleteActivity}
        submitting = {submitting}
        />
      </Container>
    </>
  )
}

export default App
