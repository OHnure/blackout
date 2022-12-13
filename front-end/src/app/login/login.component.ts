import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {UserVO} from "../model/user.model";
import {Router} from "@angular/router";

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  encapsulation: ViewEncapsulation.Emulated
})
export class LoginComponent implements OnInit {
  title = 'Blackout app';
  isAuthorize: boolean | undefined;
  user = new UserVO();
  error: string | undefined;

  constructor(private http: HttpClient, private _router: Router) {
  }

  async ngOnInit(): Promise<void> {
    this.isAuthorize = false;
    this.user = new UserVO();
  }

  public sendAuthorizationRequest ():Observable<any> {
    return this.http.post(`https://50d2-93-77-69-167.eu.ngrok.io/api/UserLogin`,  this.user, {
      headers: new HttpHeaders().set('Access-Control-Allow-Origin', '*'),
    })
  }

  public sendLoginRequest ():Observable<any> {
    return this.http.post(`https://50d2-93-77-69-167.eu.ngrok.io/api/UserLogin/${this.user.email}/${this.user.password}`, {}, {
      headers: new HttpHeaders().set('Access-Control-Allow-Origin', '*'),
    })
  }

  public authorize() {
    if(this.isValid(true)) {
      this.sendAuthorizationRequest().subscribe(res => {
        this.error = undefined;
        this._router.navigate(['notification']);
      }, error => {
        this.error = error?.error?.error;
      })
    }
  }

  public login() {
    if(this.isValid(false)) {
      this.sendLoginRequest().subscribe(res => {
        this.error = undefined;
        console.log(res);
        window.localStorage.setItem("id", res);
        this._router.navigate(['notification']);
      }, error => {
        this.error = error?.error?.error;
      })
    }
  }

  public isValid(isAuthorisation: boolean) {
    if(this.user.email.length < 4) {
      this.error = "Wrong user email format";
      return false;
    } else if(isAuthorisation && this.user.password.length < 4) {
      this.error = "Wrong user password format";
      return false;
    } else if(isAuthorisation && (this.user.repeatedPassword.length < 4 || this.user.repeatedPassword != this.user.password)) {
      this.error = "Wrong repeated password";
      return false;
    } else if(isAuthorisation && this.user.name.length < 4) {
      this.error = "Wrong user name format";
      return false;
    }

    return true;
  }

}
