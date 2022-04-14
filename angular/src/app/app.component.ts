import {Component, OnInit} from '@angular/core';
import {ReplaceableComponentsService} from "@abp/ng.core";
import {LogoComponent} from "./logo/logo.component";
import {eThemeBasicComponents} from "@abp/ng.theme.basic";

@Component({
  selector: 'app-root',
  template: `
    <abp-loader-bar></abp-loader-bar>
    <abp-dynamic-layout></abp-dynamic-layout>
  `,
})
export class AppComponent implements OnInit {
  constructor(private replaceableComponents: ReplaceableComponentsService) {} // injected ReplaceableComponentsService

  ngOnInit() {
    //...

    this.replaceableComponents.add({
      component: LogoComponent,
      key: eThemeBasicComponents.Logo,
    });
  }
}
