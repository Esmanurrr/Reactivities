import { Navigate, RouteObject, createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import HomePage from "../../features/activities/home/HomePage";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import ActivityForm from "../../features/activities/form/ActivityForm";
import ActivityDetails from "../../features/activities/details/ActivityDetails";
import TestErrors from "../../features/activities/errors/TestError";
import NotFound from "../../features/activities/errors/NotFound";
import ServerError from "../../features/activities/errors/ServerError";
import LoginForm from "../../features/activities/users/LoginForm";
import ProfilePage from "../../features/activities/profiles/ProfilePage";
import RequireAuth from "./RequireAuth";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />, //top of the tree
        children:[
            {element: <RequireAuth />, children: [
                {path: 'activities', element: <ActivityDashboard/>},
                {path: 'activities/:id', element: <ActivityDetails/>},
                {path: 'createActivity', element: <ActivityForm key='create'/>},
                {path: 'manage/:id', element: <ActivityForm key='manage' />},
                {path: 'profiles/:username', element: <ProfilePage />},
                {path: 'errors', element: <TestErrors />}
            ]},
            {path: 'not-found', element: <NotFound />},
            {path: 'server-error', element: <ServerError />},
            {path: '*', element: <Navigate replace to='/not-found' />},
        ]
    }
]

export const router = createBrowserRouter(routes);