import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { ChatComment } from "../models/comment";
import { makeAutoObservable, runInAction  } from "mobx";
import { store } from "./store";

export default class CommentStore {
    comments: ChatComment[] = [];
    hubConnection: HubConnection | null = null;

    constructor(){
        makeAutoObservable(this);
    }

    createHubConnection = (activityId:string) => {//specific SignalR connection for specific Id
        if(store.activityStore.selectedActivity){
            this.hubConnection = new HubConnectionBuilder().withUrl('http://localhost:5000/chat?activityId=' + activityId, {
                accessTokenFactory: () => store.userStore.user?.token as string
                })
                .withAutomaticReconnect()
                .configureLogging(LogLevel.Information)
                .build();
            
            this.hubConnection.start().catch(error => console.log('Error establishing the connection : ', error));

            this.hubConnection.on('LoadComments', (comments: ChatComment[]) => {
                runInAction(() => this.comments = comments); //load the all comments
            })

            this.hubConnection.on('ReceiveComment', (commnet: ChatComment) => {
                runInAction(() => this.comments.push(commnet));//we can see the new comment in real-time
            })

        }
    }

    stopHubConnection = () => {
        this.hubConnection?.stop().catch(error => console.log('Error stopping connection: ', error));
    }

    clearComments = () => {
        this.comments = [];
        this.stopHubConnection();
    }
}


