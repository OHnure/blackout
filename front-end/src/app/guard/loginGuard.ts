import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from "@angular/router";

export class LoginGuard implements CanActivate {
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    return !window.localStorage.getItem("logged")
  }
}
