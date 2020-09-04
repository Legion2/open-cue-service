{
  "x-generator": "NSwag v13.7.0.0 (NJsonSchema v10.1.24.0 (Newtonsoft.Json v9.0.0.0))",
  "openapi": "3.0.0",
  "info": {
    "title": "Open CUE Service",
    "description": "HTTP REST API service for Open CUE CLI",
    "version": "0.3.0"
  },
  "paths": {
    "/api/profiles": {
      "get": {
        "tags": [
          "Profiles"
        ],
        "operationId": "Profiles_GetAllProfiles",
        "responses": {
          "200": {
            "description": "",
            "content": {
              "application/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/Profile"
                  }
                }
              }
            }
          }
        }
      }
    },
    "/api/profiles/{name}": {
      "get": {
        "tags": [
          "Profiles"
        ],
        "operationId": "Profiles_GetProfile",
        "parameters": [
          {
            "name": "name",
            "in": "path",
            "required": true,
            "schema": {
              "type": "string",
              "nullable": true
            },
            "x-position": 1
          }
        ],
        "responses": {
          "200": {
            "description": "",
            "content": {
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/Profile"
                }
              }
            }
          }
        }
      }
    },
    "/api/profiles/{name}/trigger": {
      "post": {
        "tags": [
          "Profiles"
        ],
        "operationId": "Profiles_TriggerProfile",
        "parameters": [
          {
            "name": "name",
            "in": "path",
            "required": true,
            "schema": {
              "type": "string",
              "nullable": true
            },
            "x-position": 1
          }
        ],
        "responses": {
          "200": {
            "description": "",
            "content": {
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/Profile"
                }
              }
            }
          }
        }
      }
    },
    "/api/profiles/{name}/state": {
      "get": {
        "tags": [
          "Profiles"
        ],
        "operationId": "Profiles_GetState",
        "parameters": [
          {
            "name": "name",
            "in": "path",
            "required": true,
            "schema": {
              "type": "string",
              "nullable": true
            },
            "x-position": 1
          }
        ],
        "responses": {
          "200": {
            "description": "",
            "content": {
              "application/json": {
                "schema": {
                  "type": "boolean"
                }
              }
            }
          }
        }
      }
    },
    "/api/profiles/{name}/state/{value}": {
      "put": {
        "tags": [
          "Profiles"
        ],
        "operationId": "Profiles_SetState",
        "parameters": [
          {
            "name": "name",
            "in": "path",
            "required": true,
            "schema": {
              "type": "string",
              "nullable": true
            },
            "x-position": 1
          },
          {
            "name": "value",
            "in": "path",
            "required": true,
            "schema": {
              "type": "boolean"
            },
            "x-position": 2
          }
        ],
        "responses": {
          "200": {
            "description": "",
            "content": {
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/Profile"
                }
              }
            }
          }
        }
      }
    },
    "/api/sdk/connection": {
      "get": {
        "tags": [
          "Sdk"
        ],
        "operationId": "Sdk_GetConnection",
        "responses": {
          "200": {
            "description": "",
            "content": {
              "application/json": {
                "schema": {
                  "type": "boolean"
                }
              }
            }
          }
        }
      }
    },
    "/api/sdk/game": {
      "get": {
        "tags": [
          "Sdk"
        ],
        "operationId": "Sdk_GetGame",
        "responses": {
          "200": {
            "description": "",
            "content": {
              "application/json": {
                "schema": {
                  "type": "string"
                }
              }
            }
          }
        }
      }
    },
    "/api/sdk/details": {
      "get": {
        "tags": [
          "Sdk"
        ],
        "operationId": "Sdk_GetDetails",
        "responses": {
          "200": {
            "description": "",
            "content": {
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/CorsairProtocolDetails"
                }
              }
            }
          }
        }
      }
    },
    "/api/sdk/control": {
      "get": {
        "tags": [
          "Sdk"
        ],
        "operationId": "Sdk_GetControl",
        "responses": {
          "200": {
            "description": "",
            "content": {
              "application/json": {
                "schema": {
                  "type": "boolean"
                }
              }
            }
          }
        }
      }
    },
    "/api/sdk/control/{value}": {
      "put": {
        "tags": [
          "Sdk"
        ],
        "operationId": "Sdk_Control",
        "parameters": [
          {
            "name": "value",
            "in": "path",
            "required": true,
            "schema": {
              "type": "boolean"
            },
            "x-position": 1
          }
        ],
        "responses": {
          "200": {
            "description": "",
            "content": {
              "application/json": {
                "schema": {
                  "type": "boolean"
                }
              }
            }
          }
        }
      }
    },
    "/api/sdk/stop-all-events": {
      "post": {
        "tags": [
          "Sdk"
        ],
        "operationId": "Sdk_StopAllEvents",
        "responses": {
          "200": {
            "description": ""
          }
        }
      }
    },
    "/api/sdk/deactivate-all-profiles": {
      "post": {
        "tags": [
          "Sdk"
        ],
        "operationId": "Sdk_DeactivateAllProfiles",
        "responses": {
          "200": {
            "description": ""
          }
        }
      }
    }
  },
  "components": {
    "schemas": {
      "Profile": {
        "type": "object",
        "additionalProperties": false,
        "properties": {
          "name": {
            "type": "string"
          },
          "priority": {
            "type": "integer",
            "format": "int32"
          },
          "state": {
            "type": "boolean"
          }
        }
      },
      "CorsairProtocolDetails": {
        "type": "object",
        "additionalProperties": false,
        "properties": {
          "sdkVersion": {
            "type": "string",
            "nullable": true
          },
          "serverVersion": {
            "type": "string",
            "nullable": true
          },
          "sdkProtocolVersion": {
            "type": "integer",
            "format": "int32"
          },
          "serverProtocolVersion": {
            "type": "integer",
            "format": "int32"
          },
          "breakingChanges": {
            "type": "boolean"
          }
        }
      }
    }
  }
}