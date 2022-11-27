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
    this.sendAuthorizationRequest().subscribe(res => {
      this.error = undefined;
      this._router.navigate(['notification']);
    }, error => {
      this.error = error.message;
    })
  }
}
