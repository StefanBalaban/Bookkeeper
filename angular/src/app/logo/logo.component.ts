import { Component } from '@angular/core';

@Component({
  selector: 'app-logo',
  styleUrls: ['./logo.component.scss'],
  template: `
    <a class="navbar-brand" routerLink="/">
      <img
        src="/assets/images/tulumba.png"
        class="logo"
      />
    </a>
  `,
})
export class LogoComponent {}
