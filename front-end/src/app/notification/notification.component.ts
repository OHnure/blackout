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
  subject: string | undefined;
  currentUser: UserVO | undefined;

  constructor(private http: HttpClient, private _router: Router) {
  }

  async ngOnInit(): Promise<void> {
    window.localStorage.setItem("logged", "true");
    this.from = "";
    this.to = "";
    this.message = "";
    this.getUsersData();
    this.users = [];
  }

  getUsersData() {
    this.http.post(`https://623d-93-77-69-167.eu.ngrok.io/api/UserLogin/getAllUsers`, {},{
      headers: new HttpHeaders().set('Access-Control-Allow-Origin', '*').set("ngrok-skip-browser-warning", "*"),
    }).subscribe(data => {

      this.users = [];
      // @ts-ignore
      let users = data["users"].forEach(el => {
        if(el.id != this.getId()) {
          this.users.push(el);
        } else {
          this.currentUser = new UserVO(el);
        }
      });
    })
  }

  private getId() {
    return window.localStorage.getItem("id");
  }

  goToProfilePage() {
    this._router.navigate(['profile']);
  }

  logout() {
    window.localStorage.clear();
    this._router.navigate(['']);
  }

  notify() {
    let body = `Hello! ${this.currentUser?.name} have a blackout from ${this.from || "unknown"} to ${this.to || "unknown"}. \n City - ${this.currentUser?.city || "unknown"} \n `;
    body += this.message;
    let defaultSubject = "Notification about Blackout from" + this.currentUser?.name;

    this.http.post(`https://623d-93-77-69-167.eu.ngrok.io/api/Userlogin/sendMail`, {
      id: this.getId(),
      body: body,
      subject: this.subject || defaultSubject
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
