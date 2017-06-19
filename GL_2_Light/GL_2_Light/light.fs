
#version 330 core

out vec4 color;

in vec3 FragPos;
in vec3 Normal;

uniform vec3 objectColor;
uniform vec3 lightColor;
uniform vec3 lightPos;
uniform vec3 viewPos;


void main()
{
    // ambient
    float ambientStrenght = 0.1;
    vec3 ambient = ambientStrenght * lightColor;
    
    // diffuse
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    float diff = max(dot(norm, lightDir), 0.f);
    vec3 diffuse = diff * lightColor;
    

    // specular
    float specularStrenght = .5f;
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.f), 32);
    vec3 specular = specularStrenght * spec * lightColor;
    
    vec3 result = (ambient + diffuse + specular) * objectColor;
    
    color = vec4(result, 1.f);
}
