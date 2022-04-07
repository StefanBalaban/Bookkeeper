
#!/usr/bin/env sh

find '/usr/share/nginx/html' -name '*.js' -exec sed -i -e 's,ANGULAR_CLIENT_URL_BASE,'"$ANGULAR_CLIENT_URL_BASE"',g' {} \;
find '/usr/share/nginx/html' -name '*.js' -exec sed -i -e 's,API_HOST_URL_BASE,'"$API_HOST_URL_BASE"',g' {} \;
nginx -g "daemon off;"
