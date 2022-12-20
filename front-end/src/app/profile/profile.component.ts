import { Component } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Router} from "@angular/router";
import {UserVO} from "../model/user.model";
import {Observable} from "rxjs";

@Component({
  selector: 'profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {
  title = 'Edit profile';
  user = new UserVO();
  error: string | undefined;
  changePassword: boolean | undefined;

  constructor(private http: HttpClient, private _router: Router) {
  }

  async ngOnInit(): Promise<void> {
    window.localStorage.setItem("logged", "true");
    this.changePassword = false;
    this.getUserData();
  }

  public getUserData () {
    let id = window.localStorage.getItem("id");
    this.http.post(`https://623d-93-77-69-167.eu.ngrok.io/api/UserLogin/${id}`, {},{
      headers: new HttpHeaders().set('Access-Control-Allow-Origin', '*'),
    }).subscribe(res => {
      this.error = undefined;
      console.log(res);
      this.user = new UserVO(res);
    }, error => {
      this.error = error?.error?.error;
    });
  }

  public save() {
    if(this.isValid()) {
      this.user.id = window.localStorage.getItem("id");
      this.http.put(`https://623d-93-77-69-167.eu.ngrok.io/api/UserLogin`, this.user, {
        headers: new HttpHeaders().set('Access-Control-Allow-Origin', '*'),
      }).subscribe(res => {
        this.error = undefined;
        this._router.navigate(['notification']);
      }, error => {
        this.error = error?.error?.error;
      })
    }
  }

  public exit() {
    this._router.navigate(['notification']);
  }

  public isValid() {
    if(this.user.email.length < 4) {
      this.error = "Wrong user email format";
      return false;
    } else if(this.changePassword && this.user.password.length < 4) {
      this.error = "Wrong user password format";
      return false;
    } else if(this.changePassword && (this.user.repeatedPassword.length < 4 || this.user.repeatedPassword != this.user.password)) {
      this.error = "Wrong repeated password";
      return false;
    } else if(this.user.name.length < 4) {
      this.error = "Wrong user name format";
      return false;
    }

    return true;
  }
}
