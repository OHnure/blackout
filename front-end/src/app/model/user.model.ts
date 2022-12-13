export class UserVO {

  id: string | null;
  name: string;
  email: string;
  password: string;
  repeatedPassword: string;
  city: string;
  address: string;


  constructor(data?: any) {
    this.id = data ? data.id || 0 : 0;
    this.name = data ? data.name || "" : "";
    this.email = data ? data.email || "" : "";
    this.password = data ? data.password || "" : "";
    this.repeatedPassword = data ? data.repeatedPassword || "" : "";
    this.city = data ? data.city || "" : "";
    this.address = data ? data.address || "" : "";
  }

}
