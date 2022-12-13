import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from "./login/login.component";
import {NotificationComponent} from "./notification/notification.component";
import {ProfileComponent} from "./profile/profile.component";
import {LoginGuard} from "./guard/loginGuard";

const routes: Routes = [{
  path: '',
  component: LoginComponent,
  canActivate: [LoginGuard],
}, {
  path: 'notification',
  component: NotificationComponent,
}, {
  path: 'profile',
  component: ProfileComponent
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
