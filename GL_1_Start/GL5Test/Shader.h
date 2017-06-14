//
//  Shader.hpp
//  GL5Test
//
//  Created by jimbo on 2017/6/12.
//  Copyright © 2017年 naver. All rights reserved.
//

#ifndef Shader_h
#define Shader_h

#include <string>
#include <fstream>
#include <sstream>
#include <iostream>
#include <stdio.h>

#include <GL/glew.h> // 包含glew来获取所有的必须OpenGL头文件

class Shader
{
public:
    // 程序ID
    GLuint Program;
    // 构造器读取并构建着色器
    Shader(const GLchar* vertexPath, const GLchar* fragmentPath);
    // 使用程序
    void Use();
};

#endif /* Shader_hpp */
