import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
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
    return this.http.post(``,  this.user);
  }

  public authorize() {
    if(this.isValid(true)) {
      this.sendAuthorizationRequest().subscribe(res => {
        this.error = undefined;
        this._router.navigate(['notification']);
      }, error => {
        this.error = error.message;
      })
    }
  }

  public login() {
    if(this.isValid(false)) {

    }
  }

  public isValid(isAuthorisation: boolean) {
    if(this.user.email.length < 4) {
      this.error = "Wrong user email";
      return false;
    } else if(isAuthorisation && this.user.password.length < 4) {
      this.error = "Wrong user password";
      return false;
    } else if(isAuthorisation && this.user.repeatedPassword.length < 4 || this.user.repeatedPassword != this.user.password) {
      this.error = "Wrong repeated password";
      return false;
    } else if(this.user.name.length < 4) {
      this.error = "Wrong user name";
      return false;
    }

    return true;
  }

}
