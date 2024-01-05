import React from "react";
import { Button, Image, Card, CardContent, CardHeader } from "semantic-ui-react";
import { Activity } from "../../../../app/layout/models/activity";

interface Props{
    activity:Activity
    cancelSelectActivity: () => void;
    openForm: (id:string) => void;

}

export default function ActivityDetails({activity, cancelSelectActivity,openForm} : Props){
    return(
        <Card fluid>
            <Image src={`public/assets/categoryImages/${activity.category}.jpg`} />
            <CardContent>
                <CardHeader>{activity.title}</CardHeader>
                <Card.Meta>
                    <span>{activity.date}</span>
                </Card.Meta>
                <Card.Description>
                    {activity.description}
                </Card.Description>
            </CardContent>
            <CardContent extra>
                <Button onClick={() => openForm(activity.id)} basic color='blue' content='Edit'/>
                <Button onClick={cancelSelectActivity}basic color='grey' content='Cancel'/>
            </CardContent>
        </Card>
    )
}