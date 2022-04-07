import { Environment } from '@abp/ng.core';

const clientbaseUrl = 'ANGULAR_CLIENT_URL_BASE';
const hostbaseUrl = 'API_HOST_URL_BASE';

export const environment = {
  production: true,
  application: {
    clientbaseUrl,
    name: 'Tulumba',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: hostbaseUrl,
    redirectUri: clientbaseUrl,
    clientId: 'Tulumba_App',
    responseType: 'code',
    scope: 'offline_access openid profile role email phone Tulumba',
    requireHttps: false
  },
  apis: {
    default: {
      url: hostbaseUrl,
      rootNamespace: 'Tulumba',
    },
  },
} as Environment;
