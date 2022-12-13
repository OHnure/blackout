import { Component } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Router} from "@angular/router";
import {UserVO} from "../model/user.model";

@Component({
  selector: 'notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.scss']
})
export class NotificationComponent {
  title = 'blackout';
  users:any = [];
  from: string | undefined;
  to: string | undefined;
  message: string | undefined;
  success: string | undefined;
  error: string | undefined;

  constructor(private http: HttpClient, private _router: Router) {
  }

  async ngOnInit(): Promise<void> {
    window.localStorage.setItem("logged", "true");
    this.from = "";
    this.to = "";
    this.message = "";
    this.getUsersData();
  }

  getUsersData() {
    this.http.get(`https://50d2-93-77-69-167.eu.ngrok.io/api/UserLogin`, {
      headers: new HttpHeaders().set('Access-Control-Allow-Origin', '*'),
    }).subscribe(data => {
      this.users = data;
    })
  }

  goToProfilePage() {
    this._router.navigate(['profile']);
  }

  logout() {
    window.localStorage.clear();
    this._router.navigate(['']);
  }

  notify() {
    this.http.post(`https://50d2-93-77-69-167.eu.ngrok.io/api/notify`, {
      from: this.from,
      to: this.to,
      message: this.message
    }, {
      headers: new HttpHeaders().set('Access-Control-Allow-Origin', '*'),
    }).subscribe(data => {
      this.error = undefined
      this.success = "Notification was sent"
    }, error => {
      this.success = undefined;
      this.error = error?.error?.error;
    })
  }
}
