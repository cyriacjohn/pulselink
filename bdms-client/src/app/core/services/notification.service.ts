import * as signalR from '@microsoft/signalr';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Subject } from 'rxjs'; 

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private hubConnection!: signalR.HubConnection;
  donationUpdated = new Subject<number>();

  startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder().withUrl(`${environment.apiUrl}/notifications`, { withCredentials: true }).withAutomaticReconnect().build();
    this.hubConnection.start().then(() => console.log("SignalR connected")).catch(err => console.error('Error while starting connection: ' + err));
    this.hubConnection.on("DonationUpdated", (donationId: number, message: string) => {
      console.log("Donation updated: ", donationId);
      this.donationUpdated.next(donationId);
    });
  }
  listen() {
    this.hubConnection.on("ReceiveNotification", (message: any) => {
      alert(message)
    })
  }

  onDonationUpdate(callback: any) {
    this.hubConnection.on("DonationUpdated", callback);
  }
}
