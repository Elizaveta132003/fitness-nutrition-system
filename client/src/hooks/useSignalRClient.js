import {useState, useEffect, useContext} from 'react';
import * as signalR from '@microsoft/signalr';
import {Context} from "../index";

const useSignalRClient = () => {
    const [connection, setConnection] = useState(null);
    const [message, setMessage] = useState("");
    const [changeMessage, setChangeMessage] = useState(false)
    const {store} = useContext(Context);

    const addMessage = (msg) => setMessage(msg);

    useEffect( () => {
        const newConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:7113/caloriesHub', {
                skipNegotiation: true,
                transport: signalR.HttpTransportType.WebSockets
            })
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);

       newConnection.on('ReceiveCalories', (msg) => {
           addMessage(msg);
           setChangeMessage(true)
       });

        newConnection.start();

        return () => newConnection.stop();
    }, []);

    return { message, changeMessage, setChangeMessage}
};

export default useSignalRClient;

