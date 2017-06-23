
#version 330 core
struct Material {
    
    sampler2D diffuse;
    sampler2D specular;
    float shininess;
};

struct Light {
    vec3 position;
    vec3 direction;
    float cutOff;
    float outerCutOff;
    
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
    
    float contant;
    float linear;
    float quadratic;
    
};

out vec4 color;

in vec3 FragPos;
in vec3 Normal;
in vec2 TexCoords;


uniform vec3 viewPos;
uniform Material material;
uniform Light light;

void main()
{
    // ambient 环境光
    vec3 ambient = light.ambient * texture(material.diffuse, TexCoords).rgb;
    
    // diffuse 漫反射
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);
    //    vec3 lightDir = normalize(-light.direction);
    float diff = max(dot(norm, lightDir), 0.f);
    vec3 diffuse = light.diffuse * diff * texture(material.diffuse, TexCoords).rgb;
    
    // specular 镜面光
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.f), material.shininess);
    vec3 specular = light.specular * spec * texture(material.specular, TexCoords).rgb;
    

    
    // spotlight (soft edges)
    float theta = dot(lightDir, normalize(-light.direction));
    float epsilon = light.cutOff - light.outerCutOff;
    float intensity = clamp((theta - light.outerCutOff) / epsilon, 0.f, 1.f)
    diffuse  *= intensity;
    specular *= intensity;
    
    // attenuation
    float  distance = length(light.position - FragPos);
    float attenuation = 1.f / (light.contant + light.linear * distance +   light.quadratic * (distance * distance));
    ambient  *= attenuation;
    diffuse  *= attenuation;
    specular *= attenuation;
    
//    
//    if (theta > light.cutOff) {
//        //执行光照
//        vec3 result = ambient + diffuse + specular;
//        color = vec4(result, 1.f);
//    } else // 否则，使用环境光，让场景在聚光之外时不至于完全黑暗
//        color = vec4(light.ambient * vec3(texture(material.diffuse, TexCoords)), 1.f);
//    
    vec3 result = ambient + diffuse + specular;
    color = vec4(result, 1.f);
}
