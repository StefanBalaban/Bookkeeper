import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'Tulumba',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44389',
    redirectUri: baseUrl,
    clientId: 'Tulumba_App',
    responseType: 'code',
    scope: 'offline_access openid profile role email phone Tulumba',
    requireHttps: false
  },
  apis: {
    default: {
      url: 'https://localhost:44389',
      rootNamespace: 'Tulumba',
    },
  },
} as Environment;
