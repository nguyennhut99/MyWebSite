import { User, UserManager } from "oidc-client";
import configData from "../config.json";

const oidcSettings = {
  authority: configData.Back_end,
  client_id: "react_code_client",
  redirect_uri: `${configData.React}/authentication/login-callback`,
  post_logout_redirect_uri: `${configData.React}/authentication/logout-callback`,
  response_type: "code",
  scope: "myshop.api openid profile",
  automaticSilentRenew: true,
  includeIdTokenInSilentRenew: true,
};

export class AuthService {
  public userManager: UserManager;

  constructor() {
    this.userManager = new UserManager(oidcSettings);
  }

  public getUserAsync(): Promise<User | null> {
    return this.userManager.getUser();
  }

  public loginAsync(): Promise<void> {
    return this.userManager.signinRedirect();
  }

  public completeLoginAsync(url: string): Promise<User> {
    return this.userManager.signinCallback(url);
  }

  public renewTokenAsync(): Promise<User> {
    return this.userManager.signinSilent();
  }

  public logoutAsync(): Promise<void> {
    return this.userManager.signoutRedirect();
  }

  public async completeLogoutAsync(url: string): Promise<void> {
    await this.userManager.signoutCallback(url);
  }
}