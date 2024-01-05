import React from "react";
import { Button, Image, Card, CardContent, CardHeader } from "semantic-ui-react";
import { Activity } from "../../../../app/layout/models/activity";

interface Props{
    activity:Activity
}

export default function ActivityDetails({activity} : Props){
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
                <Button basic color='blue' content='Edit'/>
                <Button basic color='grey' content='Cancel'/>
            </CardContent>
        </Card>
    )
}